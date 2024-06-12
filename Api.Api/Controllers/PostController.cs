using System.Linq.Expressions;
using System.Net;
using System.Security.Cryptography;
using Api.Api.Responses;
using Api.Common.Exceptions;
using Api.Common.Functions;
using Api.Common.Interfaces.Repositories;
using Api.Common.Interfaces.Services;
using Api.Domain.Dto.Request.Create;
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
    private readonly IPostService _service;
    private readonly ITokenHelperService _tokenHelper;
    private readonly ILocalStorageService _localStorageService;

    public PostController(IMapper mapper, IPostService service, ITokenHelperService tokenHelper, ILocalStorageService localStorageService)
    {
        this._mapper = mapper;
        this._service = service;
        this._tokenHelper = tokenHelper;
        this._localStorageService = localStorageService;
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

}