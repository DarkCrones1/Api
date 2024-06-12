using System.Net;

using Microsoft.AspNetCore.Mvc;

using Api.Common.Dtos.Response;
using Api.Domain.Interfaces.Services;
using Api.Api.Responses;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;

namespace Api.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
[Produces("application/json")]
public class MiscellaneousController : ControllerBase
{
    private readonly IMiscellaneousService _service;

    public MiscellaneousController(IMiscellaneousService service)
    {
        this._service = service;
    }

    [HttpGet]
    [Route("Gender")]
    [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(ApiResponse<IEnumerable<EnumValueResponseDto>>))]
    [ProducesResponseType((int)HttpStatusCode.BadRequest, Type = typeof(ApiResponse<IEnumerable<EnumValueResponseDto>>))]
    [ProducesResponseType((int)HttpStatusCode.NotFound, Type = typeof(ApiResponse<IEnumerable<EnumValueResponseDto>>))]
    [ResponseCache(Duration = 300)]
    public async Task<IActionResult> GetGender()
    {
        var lstItem = await _service.GetGender();
        var response = new ApiResponse<IEnumerable<EnumValueResponseDto>>(lstItem);
        return Ok(response);
    }

}