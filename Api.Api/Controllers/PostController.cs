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
[Authorize]
public class PostController : ControllerBase
{
    private readonly IMapper _mapper;
    private readonly IConfiguration _configuration;
    private readonly IPostService _service;
    private readonly ITokenHelperService _tokenHelper;
    private readonly ILocalStorageService _localStorageService;

    public PostController(IMapper mapper, IConfiguration configuration, IPostService service, ITokenHelperService tokenHelper, ILocalStorageService localStorageService)
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
    [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(ApiResponse<IEnumerable<PostResponseDto>>))]
    [ProducesResponseType((int)HttpStatusCode.BadRequest, Type = typeof(ApiResponse<IEnumerable<PostResponseDto>>))]
    [ProducesResponseType((int)HttpStatusCode.NotFound, Type = typeof(ApiResponse<IEnumerable<PostResponseDto>>))]
    public async Task<IActionResult> GetAll([FromQuery] PostQueryFilter filter)
    {
        try
        {
            var entities = await _service.GetPaged(filter);
            var dtos = _mapper.Map<IEnumerable<PostResponseDto>>(entities);
            var metaDataResponse = new MetaDataResponse(
                entities.TotalCount,
                entities.CurrentPage,
                entities.PageSize
            );
            var response = new ApiResponse<IEnumerable<PostResponseDto>>(data: dtos, meta: metaDataResponse);
            return Ok(response);
        }
        catch (Exception ex)
        {
            throw new LogicBusinessException(ex);
        }
    }

    [HttpGet]
    [Route("{id:int}")]
    [AllowAnonymous]
    [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(ApiResponse<IEnumerable<PostDetailResponseDto>>))]
    [ProducesResponseType((int)HttpStatusCode.BadRequest, Type = typeof(ApiResponse<IEnumerable<PostDetailResponseDto>>))]
    [ProducesResponseType((int)HttpStatusCode.NotFound, Type = typeof(ApiResponse<IEnumerable<PostDetailResponseDto>>))]
    public async Task<IActionResult> GetById([FromRoute] int id)
    {
        try
        {
            var entity = await _service.GetById(id);

            if (entity.Id <= 0)
                return NotFound();

            var dto = _mapper.Map<PostDetailResponseDto>(entity);
            var response = new ApiResponse<PostDetailResponseDto>(data: dto);
            return Ok(response);
        }
        catch (Exception ex)
        {
            throw new LogicBusinessException(ex);
        }
    }

    [HttpPost]
    [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(ApiResponse<PostResponseDto>))]
    public async Task<IActionResult> CreatePost([FromBody] PostCreateRequestDto requestDto)
    {
        try
        {
            var entity = _mapper.Map<Post>(requestDto);
            entity.CreatedBy = _tokenHelper.GetUserName();
            entity.UserAccountId = _tokenHelper.GetAccountId();
            await _service.Create(entity);

            var result = _mapper.Map<PostResponseDto>(entity);
            var Response = new ApiResponse<PostResponseDto>(result);
            return Ok(Response);
        }
        catch (Exception ex)
        {
            throw new LogicBusinessException(ex);
        }

    }

    [HttpPut]
    [Route("{id:int}")]
    [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(ApiResponse<PostResponseDto>))]
    public async Task<IActionResult> Update([FromRoute] int id, [FromBody] PostUpdateRequestDto requestDto)
    {
        try
        {
            Expression<Func<Post, bool>> filter = x => x.Id == id;
            var existInfo = await _service.Exist(filter);

            if (!existInfo)
                return BadRequest("No se encontro ningununa información de usuario");

            var entity = await _service.GetById(id);

            var newEntity = _mapper.Map<Post>(requestDto);
            newEntity.IsDeleted = false;
            newEntity.Id = id;
            newEntity.UserAccountId = entity.UserAccountId;
            newEntity.LastModifiedBy = _tokenHelper.GetUserName();
            newEntity.LastModifiedDate = DateTime.Now;

            await _service.Update(newEntity);
            var dto = _mapper.Map<PostResponseDto>(newEntity);
            var response = new ApiResponse<PostResponseDto>(data: dto);
            return Ok(response);
        }
        catch (Exception ex)
        {

            throw new LogicBusinessException(ex);
        }
    }

    [HttpDelete]
    [Route("{id:int}")]
    public async Task<IActionResult> Delete([FromRoute] int id)
    {
        try
        {
            Expression<Func<Post, bool>> filter = x => x.Id == id;
            var existInfo = await _service.Exist(filter);

            if (!existInfo)
                return BadRequest("No se encontro ningununa información de usuario");

            var entity = await _service.GetById(id);
            entity.IsDeleted = true;
            entity.Id = id;
            entity.LastModifiedBy = _tokenHelper.GetUserName();
            entity.LastModifiedDate = DateTime.Now;

            await _service.Update(entity);
            return Ok(true);
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
    [Route("UploadImagePost")]
    public async Task<IActionResult> UploadImage([FromForm] ImageCreateRequestDto requestDto)
    {
        try
        {
            var urlFile = await _localStorageService.UploadAsync(requestDto.File, LocalContainer.Image_Post, Guid.NewGuid().ToString());

            string url = $"{GetUrlBaseLocal((short)LocalContainer.Image_Post)}{urlFile}";

            await _service.UpdatePost(requestDto.EntityAssigmentId, url, _tokenHelper.GetUserName());

            return Ok();
        }
        catch (Exception ex)
        {

            throw new LogicBusinessException(ex);
        }
    }

    [HttpPut]
    [Route("UpdateImagePost")]
    public async Task<IActionResult> UpdateImage([FromForm] ImageCreateRequestDto requestDto)
    {
        try
        {
            Expression<Func<Post, bool>> filter = x => x.Id == requestDto.EntityAssigmentId;
            var existPost = await _service.Exist(filter);

            if (!existPost)
                return BadRequest("No se encontró ninguna publicación");

            var entity = await _service.GetById(requestDto.EntityAssigmentId);

            var urlFile = await _localStorageService.EditFileAsync(requestDto.File, LocalContainer.Image_Post, entity.ImagePostUrl!);

            string url = $"{GetUrlBaseLocal((short)LocalContainer.Image_Post)}{urlFile}";

            await _service.UpdatePost(requestDto.EntityAssigmentId, url, _tokenHelper.GetUserName());

            return Ok();
        }
        catch (Exception ex)
        {

            throw new LogicBusinessException(ex);
        }
    }

    [HttpDelete]
    [Route("{id:int}/DeleteImagePost")]
    public async Task<IActionResult> DeleteImage([FromRoute] int id)
    {
        try
        {
            Expression<Func<Post, bool>> filter = x => x.Id == id;
            var existPost = await _service.Exist(filter);

            if (!existPost)
                return BadRequest("No se encontro ninguna publicación");

            var entity = await _service.GetById(id);
            entity.LastModifiedBy = _tokenHelper.GetUserName();
            entity.LastModifiedDate = DateTime.Now;
            entity.IsDeleted = true;
            entity.Id = id;

            await _localStorageService.DeteleAsync(LocalContainer.Image_Post, entity.ImagePostUrl!);
            return Ok(true);
        }
        catch (Exception ex)
        {

            throw new LogicBusinessException(ex);
        }
    }

}