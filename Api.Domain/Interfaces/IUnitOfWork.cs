using Api.Common.Interfaces.Repositories;
// using Api.Domain.Entities;
// using Api.Domain.Interfaces.Repositories;

namespace Api.Domain.Interfaces;

public interface IUnitOfWork : IDisposable
{

    ILocalStorageRepository LocalStorageRepository { get; }

    void SaveChanges();

    Task SaveChangesAsync();
}