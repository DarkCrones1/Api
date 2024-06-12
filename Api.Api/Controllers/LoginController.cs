using System.Linq.Expressions;
using System.Net;

using Microsoft.AspNetCore.Mvc;

using AutoMapper;

using Api.Common.Interfaces.Services;
using Api.Domain.Dto.Response;
using Api.Domain.Entities;
using Api.Api.Responses;
using Api.Domain.Dto.Request.Create;
using Api.Common.Exceptions;
using Api.Common.Helpers;
using Microsoft.AspNetCore.Authorization;
using Api.Domain.Dto.QueryFilters;
using Api.Domain.Interfaces.Services;
using Api.Domain.Dto.Request;
using Api.Api.Helpers;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using System.Security.Claims;
using Api.Common.Functions;
using AW.Domain.Dto.Request;

namespace Api.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
[Produces("application/json")]
public class LoginController : ControllerBase
{
    private readonly IMapper _mapper;
    private readonly IAuthenticationService _service;
    private readonly IUserAccountService _userAccountService;
    private readonly IConfiguration _configuration;
    protected ActiveUserAccount _user;

    public LoginController(IMapper mapper, IAuthenticationService service, IUserAccountService userAccountService, IConfiguration configuration)
    {
        this._mapper = mapper;
        this._service = service;
        this._userAccountService = userAccountService;
        this._configuration = configuration;
        _user = new ActiveUserAccount();
        SettingConfigurationFile.Initialize(_configuration);
    }

    [HttpPost]
    [Route("")]
    public async Task<IActionResult> Login([FromBody] LoginRequestDto requestDto)
    {
        try
        {
            var result = await _service.IsValidUser(requestDto.UserNameOrEmail!, MD5Encrypt.GetMD5(requestDto.Password!));

            if (!result)
                return NotFound("El Usuario no es válido, revise que el Usuario/Email o la Contraseña sean correctos");

            _user = await GetUserAccount(requestDto);

            var token = await GenerateToken();
            return Ok(new { token });
        }
        catch (Exception)
        {

            throw new LogicBusinessException("No se ha encontrado ningun usuario");
        }
    }

    private async Task<string> GenerateToken()
    {
        // Header
        var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Authentication:SecretKey"]!));
        var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);
        var header = new JwtHeader(signingCredentials);

        var lstClaims = new List<Claim>{
                new Claim(ClaimTypes.NameIdentifier, _user!.UserName),
                new Claim(ClaimTypes.Name, _user!.Name),
                new Claim(ClaimTypes.Email, _user.Email),
                new Claim(ClaimTypes.Sid, $"{_user.Id}"),
                new Claim(ClaimTypes.DateOfBirth, DateTime.Now.ToString()),
                //new Claim("", "") //TODO: agregar valores personalizados
                new Claim("UserInfoId", $"{_user.UserInfoId}")
            };

        // Payload
        var elapsedTime = int.Parse(_configuration["Authentication:ExpirationMinutes"]!);

        var payload = new JwtPayload(
            issuer: _configuration["Authentication:Issuer"],
            audience: _configuration["Authentication:Audience"],
            claims: lstClaims,
            notBefore: DateTime.Now,
            expires: DateTime.UtcNow.AddMinutes(elapsedTime)
        );

        // Signature
        var token = new JwtSecurityToken(header, payload);

        await Task.CompletedTask;

        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    private async Task<ActiveUserAccount> GetUserAccount(LoginRequestDto requestDto)
    {
        Expression<Func<ActiveUserAccount, bool>> filters = x => x.UserName == requestDto.UserNameOrEmail || x.Email == requestDto.UserNameOrEmail;
        var entity = await _userAccountService.GetUserAccountToLogin(filters);
        return entity;
    }
}