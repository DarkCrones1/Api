using System.Security.Claims;
using Api.Common.Interfaces.Services;
using Microsoft.AspNetCore.Http;

namespace Api.Common.Helpers;

public class TokenHelper : ITokenHelperService
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public TokenHelper(IHttpContextAccessor httpContextAccessor)
    {
        this._httpContextAccessor = httpContextAccessor;
    }

    public string GetFullName()
    {
        var identity = _httpContextAccessor.HttpContext!.User.Identity as ClaimsIdentity;
        var userName = identity!.FindFirst(ClaimTypes.Name);

        return userName!.Value;
    }

    public string GetUserName()
    {
        var identity = _httpContextAccessor.HttpContext!.User.Identity as ClaimsIdentity;
        var nameIdentifier = identity!.FindFirst(ClaimTypes.NameIdentifier);

        return nameIdentifier!.Value;
    }

    public int GetAccountId()
    {
        var identity = _httpContextAccessor.HttpContext!.User.Identity as ClaimsIdentity;
        var accountId = identity!.FindFirst(ClaimTypes.Sid);

        return int.Parse(accountId!.Value);
    }

    public string GetUserAccountType()
    {
        var identity = _httpContextAccessor.HttpContext!.User.Identity as ClaimsIdentity;
        var accountType = identity!.FindFirst("UserAccountType");

        return accountType!.Value;
    }

    public int GetCustomerId()
    {
        var identity = _httpContextAccessor.HttpContext!.User.Identity as ClaimsIdentity;
        var customerId = identity!.FindFirst("CustomerId");

        return int.Parse(customerId!.Value);
    }

    public int GetCraftmanId()
    {
        var identity = _httpContextAccessor.HttpContext!.User.Identity as ClaimsIdentity;
        var craftmanId = identity!.FindFirst("CraftmanId");

        return int.Parse(craftmanId!.Value);
    }
}