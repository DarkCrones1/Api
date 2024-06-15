namespace Api.Common.Interfaces.Services;

public interface ITokenHelperService
{
    public string GetFullName();
    public string GetUserName();
    public int GetAccountId();
    public int GetUserInfoId();
}