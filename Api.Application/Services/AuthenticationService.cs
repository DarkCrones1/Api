using System.Linq.Expressions;
using Microsoft.AspNetCore.Mvc;
using Api.Common.Entities;
using Api.Domain.Dto.QueryFilters;
using Api.Domain.Entities;
using Api.Domain.Interfaces;
using Api.Domain.Interfaces.Services;

namespace Api.Application.Services;

public class AuthenticationService : IAuthenticationService
{
    private readonly IUnitOfWork unitOfWork;

    public AuthenticationService(IUnitOfWork unitOfWork)
    {
        this.unitOfWork = unitOfWork;
    }

    public async Task<bool> IsValidUser(string UserNameOrEmail, string password)
    {
        Expression<Func<UserAccount, bool>> filters = x =>
                (x.UserName == UserNameOrEmail || x.Email == UserNameOrEmail)
                && x.Password == password
                && x.IsActive
                && x.IsAuthorized
                && !x.IsDeleted!.Value;

        var result = await unitOfWork.UserAccountRepository.Exist(filters);

        return result;
    }
}