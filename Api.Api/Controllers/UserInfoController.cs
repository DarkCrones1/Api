using System.Linq.Expressions;
using System.Net;
using System.Security.Cryptography;
using Api.Api.Responses;
using Api.Common.Enumerations;
using Api.Common.Exceptions;
using Api.Common.Functions;
using Api.Common.Interfaces.Repositories;
using Api.Common.Interfaces.Services;
using Api.Domain.Dto.QueryFilters;
using Api.Domain.Dto.Request.Create;
using Api.Domain.Dto.Request.Update;
using Api.Domain.Dto.Response;
using Api.Domain.Entities;
using Api.Domain.Interfaces.Services;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
[Produces("application/json")]
public class UserInfoController : ControllerBase
{
    private readonly IMapper _mapper;
    private readonly IConfiguration _configuration;
    private readonly IUserInfoService _service;
    private readonly ITokenHelperService _tokenHelper;
    private readonly ILocalStorageService _localStorageService;

    public UserInfoController(IMapper mapper, IConfiguration configuration, IUserInfoService service, ITokenHelperService tokenHelper, ILocalStorageService localStorageService)
    {
        this._mapper = mapper;
        this._configuration = configuration;
        this._service = service;
        this._tokenHelper = tokenHelper;
        this._localStorageService = localStorageService;
    }

    [HttpGet]
    [Route("")]
    [AllowAnonymous]
    [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(ApiResponse<IEnumerable<UserInfoResponseDto>>))]
    [ProducesResponseType((int)HttpStatusCode.BadRequest, Type = typeof(ApiResponse<IEnumerable<UserInfoResponseDto>>))]
    [ProducesResponseType((int)HttpStatusCode.NotFound, Type = typeof(ApiResponse<IEnumerable<UserInfoResponseDto>>))]
    public async Task<IActionResult> GetAll([FromQuery] UserInfoQueryFilter filter)
    {
        var entities = await _service.GetPaged(filter);
        var dtos = _mapper.Map<IEnumerable<UserInfoResponseDto>>(entities);
        var metaDataResponse = new MetaDataResponse(
            entities.TotalCount,
            entities.CurrentPage,
            entities.PageSize
        );
        var response = new ApiResponse<IEnumerable<UserInfoResponseDto>>(data: dtos, meta: metaDataResponse);
        return Ok(response);
    }

    [HttpPut]
    [Route("{id:int}")]
    [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(ApiResponse<UserInfoResponseDto>))]
    public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UserInfoUpdateRequestDto requestDto)
    {
        try
        {
            Expression<Func<UserInfo, bool>> filter = x => x.Id == id;
            var existInfo = await _service.Exist(filter);

            if (!existInfo)
                return BadRequest("No se encontro ningununa información de usuario");

            var entity = await _service.GetById(id);

            var newEntity = _mapper.Map<UserInfo>(requestDto);
            newEntity.IsDeleted = false;
            newEntity.Id = id;
            newEntity.LastModifiedBy = _tokenHelper.GetUserName();
            newEntity.LastModifiedDate = DateTime.Now;
            newEntity.Code = entity.Code;

            await _service.Update(newEntity);
            var dto = _mapper.Map<UserInfoResponseDto>(newEntity);
            var response = new ApiResponse<UserInfoResponseDto>(data: dto);
            return Ok(response);
        }
        catch (Exception ex)
        {

            throw new LogicBusinessException(ex);
        }
    }

    private string GetUrlBaseLocal(int type)
    {
        var url = type switch
        {
            1 => _configuration.GetValue<string>("DefaultValues:ImagePostLocalStorageBaseUrl"),
            2 => _configuration.GetValue<string>("DefaultValues:ImageProfileLocalStorageBaseUrl"),
            _ => _configuration.GetValue<string>("DefaultValues:ImageCommentaryLocalStorageBaseUrl"),
        };
        return url!;
    }

    private static LocalContainer GetLocalContainer(int value)
    {
        return value switch
        {
            1 => LocalContainer.Image_Post,
            2 => LocalContainer.Image_Profile,
            _ => LocalContainer.Image_Commentary
        };
    }

    [HttpPost]
    [Route("UploadImageProfile")]
    public async Task<IActionResult> UploadImage([FromForm] ImageCreateRequestDto requestDto)
    {
        try
        {
            var urlFile = await _localStorageService.UploadAsync(requestDto.File, LocalContainer.Image_Profile, Guid.NewGuid().ToString());

            string url = $"{GetUrlBaseLocal((short)LocalContainer.Image_Profile)}{urlFile}";

            await _service.UpdateProfile(requestDto.EntityAssigmentId, url, _tokenHelper.GetUserName());

            return Ok();
        }
        catch (Exception ex)
        {

            throw new LogicBusinessException(ex);
        }
    }

    [HttpPut]
    [Route("UpdateImageProfile")]
    public async Task<IActionResult> UpdateImage([FromForm] ImageCreateRequestDto requestDto)
    {
        try
        {
            Expression<Func<UserInfo, bool>> filter = x => x.Id == requestDto.EntityAssigmentId;
            var existPost = await _service.Exist(filter);

            if (!existPost)
                return BadRequest("No se encontró ningun usuario");

            var entity = await _service.GetById(requestDto.EntityAssigmentId);

            var urlFile = await _localStorageService.EditFileAsync(requestDto.File, LocalContainer.Image_Profile, entity.ProfilePictureUrl!);

            string url = $"{GetUrlBaseLocal((short)LocalContainer.Image_Profile)}{urlFile}";

            await _service.UpdateProfile(requestDto.EntityAssigmentId, url, _tokenHelper.GetUserName());

            return Ok();
        }
        catch (Exception ex)
        {

            throw new LogicBusinessException(ex);
        }
    }

    [HttpDelete]
    [Route("{id:int}/DeleteImageProfile")]
    public async Task<IActionResult> DeleteImage([FromRoute] int id)
    {
        try
        {
            Expression<Func<UserInfo, bool>> filter = x => x.Id == id;
            var existPost = await _service.Exist(filter);

            if (!existPost)
                return BadRequest("No se encontro ningun usuario");

            var entity = await _service.GetById(id);
            entity.LastModifiedBy = _tokenHelper.GetUserName();
            entity.LastModifiedDate = DateTime.Now;
            entity.IsDeleted = true;
            entity.Id = id;

            await _localStorageService.DeteleAsync(LocalContainer.Image_Post, entity.ProfilePictureUrl!);
            return Ok(true);
        }
        catch (Exception ex)
        {

            throw new LogicBusinessException(ex);
        }
    }
}