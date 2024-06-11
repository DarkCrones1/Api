namespace Api.Common.Interfaces.Services;

public interface ITokenHelperService
{
    public string GetFullName();
    public string GetUserName();
    public int GetAccountId();
    public string GetUserAccountType();
    public int GetCustomerId();
    public int GetCraftmanId();
}