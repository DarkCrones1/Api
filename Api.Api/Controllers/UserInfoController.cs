using System.Linq.Expressions;
using System.Net;
using System.Security.Cryptography;
using Api.Api.Responses;
using Api.Common.Exceptions;
using Api.Common.Functions;
using Api.Common.Interfaces.Repositories;
using Api.Common.Interfaces.Services;
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
    private readonly ICrudService<UserInfo> _service;
    private readonly ITokenHelperService _tokenHelper;
    private readonly ILocalStorageService _localStorageService;

    public UserInfoController(IMapper mapper, ICrudService<UserInfo> service, ITokenHelperService tokenHelper, ILocalStorageService localStorageService)
    {
        this._mapper = mapper;
        this._service = service;
        this._tokenHelper = tokenHelper;
        this._localStorageService = localStorageService;
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
}