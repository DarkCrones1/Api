using Microsoft.Extensions.Configuration;

using Api.Common.Interfaces.Repositories;
// using Api.Domain.Entities;
using Api.Domain.Interfaces;
// using Api.Domain.Interfaces.Repositories;
// using Api.Infrastructure.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Api.Infrastructure.Data;

namespace AW.Infrastructure.Repositories;

public class UnitOfWork : IUnitOfWork
{
    protected readonly ApiDbContext _dbContext;

    private readonly IConfiguration _configuration;

    private readonly IWebHostEnvironment _env;

    private readonly IHttpContextAccessor _httpContextAccessor;

    protected ILocalStorageRepository _localStorageRepository;

    private bool disposed;

    public UnitOfWork(ApiDbContext dbContext, IConfiguration configuration, IWebHostEnvironment env, IHttpContextAccessor httpContextAccessor)
    {
        _dbContext = dbContext;

        this._configuration = configuration;

        this._env = env;

        this._httpContextAccessor = httpContextAccessor;

        disposed = false;

        _localStorageRepository = new LocalStorageRepository(_configuration, _env, _httpContextAccessor);
    }

    public ILocalStorageRepository LocalStorageRepository => _localStorageRepository;

    protected virtual void Dispose(bool disposing)
    {
        if (!this.disposed)
            if (disposing)
                _dbContext.Dispose();

        this.disposed = true;
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    public void SaveChanges()
    {
        _dbContext.SaveChanges();
    }

    public async Task SaveChangesAsync()
    {
        await _dbContext.SaveChangesAsync();
    }
}