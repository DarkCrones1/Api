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
public class MotoController : ControllerBase
{
    private readonly IMapper _mapper;
    private readonly IConfiguration _configuration;
    private readonly ICatalogBaseService<Moto> _service;
    private readonly ITokenHelperService _tokenHelper;
    private readonly ILocalStorageService _localStorageService;

    public MotoController(IMapper mapper, IConfiguration configuration, ICatalogBaseService<Moto> service, ITokenHelperService tokenHelper, ILocalStorageService localStorageService)
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
    [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(ApiResponse<IEnumerable<MotoResponseDto>>))]
    [ProducesResponseType((int)HttpStatusCode.BadRequest, Type = typeof(ApiResponse<IEnumerable<MotoResponseDto>>))]
    [ProducesResponseType((int)HttpStatusCode.NotFound, Type = typeof(ApiResponse<IEnumerable<MotoResponseDto>>))]
    public async Task<IActionResult> Getll([FromQuery] MotoQueryFilter filter)
    {
        try
        {
            var filters = _mapper.Map<Moto>(filter);
            var entities = await _service.GetPaged(filters);
            var dtos = _mapper.Map<IEnumerable<MotoResponseDto>>(entities);
            var response = new ApiResponse<IEnumerable<MotoResponseDto>>(dtos);
            return Ok(response);
        }
        catch (Exception ex)
        {
            throw new LogicBusinessException(ex);
        }
    }

    [HttpPost]
    [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(ApiResponse<MotoResponseDto>))]
    public async Task<IActionResult> CreateMoto([FromBody] MotoCreateRequestDto requestDto)
    {
        try
        {
            var entity = _mapper.Map<Moto>(requestDto);
            await _service.Create(entity);

            var result = _mapper.Map<MotoResponseDto>(entity);
            var response = new ApiResponse<MotoResponseDto>(result);
            return Ok(response);
        }
        catch (Exception ex)
        {
            throw new LogicBusinessException(ex);
        }
    }
}