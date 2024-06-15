using Api.Common.Entities;
using Api.Common.Interfaces.Services;
using Api.Domain.Dto.QueryFilters;
using Api.Domain.Entities;

namespace Api.Domain.Interfaces.Services;

public interface IUserInfoService : ICrudService<UserInfo>
{
    Task<PagedList<UserInfo>> GetPaged(UserInfoQueryFilter filter);
    Task UpdateProfile(int userInfoId, string urlProfile, string userName);
}