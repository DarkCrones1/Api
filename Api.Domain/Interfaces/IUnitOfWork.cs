using Api.Common.Interfaces.Repositories;
using Api.Domain.Entities;
using Api.Domain.Interfaces.Repositories;

namespace Api.Domain.Interfaces;

public interface IUnitOfWork : IDisposable
{
    ICrudRepository<Commentary> CommentaryRepository { get; }

    IPostRepository PostRepository { get; }

    IUserAccountRepository UserAccountRepository { get; }

    ICrudRepository<UserInfo> UserInfoRepository { get; }

    IRetrieveRepository<ActiveUserAccount> ActiveUserAccountRepository { get; }

    ILocalStorageRepository LocalStorageRepository { get; }

    void SaveChanges();

    Task SaveChangesAsync();
}