using Api.Common.Entities;
using Api.Common.Interfaces.Services;
using Api.Domain.Dto.QueryFilters;
using Api.Domain.Entities;
using Api.Domain.Enumerations;
using Api.Domain.Interfaces;
using Api.Domain.Interfaces.Services;

namespace Api.Application.Services;

public class UserInfoService : CrudService<UserInfo>, IUserInfoService
{
    public UserInfoService(IUnitOfWork unitOfWork) : base(unitOfWork)
    {
    }

    public async Task<PagedList<UserInfo>> GetPaged(UserInfoQueryFilter filter)
    {
        var result = await _unitOfWork.UserInfoRepository.GetPaged(filter);
        var pagedItems = PagedList<UserInfo>.Create(result, filter.PageNumber, filter.PageSize);
        return pagedItems;
    }

    public async Task UpdateProfile(int CustomerId, string urlProfile, string userName)
    {
        var lastEntity = await _unitOfWork.UserInfoRepository.GetById(CustomerId);

        lastEntity.ProfilePictureUrl = urlProfile;
        lastEntity.LastModifiedDate = DateTime.Now;
        lastEntity.LastModifiedBy = userName;

        await base.Update(lastEntity);
        await _unitOfWork.SaveChangesAsync();
    }
}