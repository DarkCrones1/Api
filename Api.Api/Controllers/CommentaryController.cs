using System.Linq.Expressions;
using System.Net;
using System.Security.Cryptography;
using Api.Api.Responses;
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
public class CommentaryController : ControllerBase
{
    private readonly IMapper _mapper;
    private readonly ICommentaryService _service;
    private readonly ITokenHelperService _tokenHelper;
    private readonly ILocalStorageService _localStorageService;

    public CommentaryController(IMapper mapper, ICommentaryService service, ITokenHelperService tokenHelper, ILocalStorageService localStorageService)
    {
        this._mapper = mapper;
        this._service = service;
        this._tokenHelper = tokenHelper;
        this._localStorageService = localStorageService;
    }

    [HttpGet]
    [Route("")]
    [AllowAnonymous]
    [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(ApiResponse<IEnumerable<CommentaryResponseDto>>))]
    [ProducesResponseType((int)HttpStatusCode.BadRequest, Type = typeof(ApiResponse<IEnumerable<CommentaryResponseDto>>))]
    [ProducesResponseType((int)HttpStatusCode.NotFound, Type = typeof(ApiResponse<IEnumerable<CommentaryResponseDto>>))]
    public async Task<IActionResult> GetAll([FromQuery] CommentaryQueryFilter filter)
    {
        var entities = await _service.GetPaged(filter);
        var dtos = _mapper.Map<IEnumerable<CommentaryResponseDto>>(entities);
        var metaDataResponse = new MetaDataResponse(
            entities.TotalCount,
            entities.CurrentPage,
            entities.PageSize
        );
        var response = new ApiResponse<IEnumerable<CommentaryResponseDto>>(data: dtos, meta: metaDataResponse);
        return Ok(response);
    }

    [HttpPost]
    [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(ApiResponse<CommentaryResponseDto>))]
    public async Task<IActionResult> CreatePost([FromBody] CommentaryCreateRequestDto requestDto)
    {
        try
        {
            var entity = _mapper.Map<Commentary>(requestDto);
            entity.CreatedBy = _tokenHelper.GetUserName();
            entity.UserAccountId = _tokenHelper.GetAccountId();
            await _service.Create(entity);

            var result = _mapper.Map<CommentaryResponseDto>(entity);
            var Response = new ApiResponse<CommentaryResponseDto>(result);
            return Ok(Response);
        }
        catch (Exception ex)
        {
            throw new LogicBusinessException(ex);
        }
    }

    [HttpPut]
    [Route("{id:int}")]
    [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(ApiResponse<CommentaryResponseDto>))]
    public async Task<IActionResult> Update([FromRoute] int id, [FromBody] CommentaryUpdateRequestDto requestDto)
    {
        try
        {
            Expression<Func<Commentary, bool>> filter = x => x.Id == id;
            var existInfo = await _service.Exist(filter);

            if (!existInfo)
                return BadRequest("No se encontro ningununa información de usuario");

            var entity = await _service.GetById(id);

            var newEntity = _mapper.Map<Commentary>(requestDto);
            newEntity.IsDeleted = false;
            newEntity.Id = id;
            newEntity.UserAccountId = entity.UserAccountId;
            newEntity.PostId = entity.PostId;
            newEntity.LastModifiedBy = _tokenHelper.GetUserName();
            newEntity.LastModifiedDate = DateTime.Now;

            await _service.Update(newEntity);
            var dto = _mapper.Map<CommentaryResponseDto>(newEntity);
            var response = new ApiResponse<CommentaryResponseDto>(data: dto);
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
            Expression<Func<Commentary, bool>> filter = x => x.Id == id;
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
}