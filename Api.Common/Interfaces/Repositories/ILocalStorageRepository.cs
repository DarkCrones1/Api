using Api.Common.Enumerations;
using Microsoft.AspNetCore.Http;

namespace Api.Common.Interfaces.Repositories;

public interface ILocalStorageRepository
{
    Task DeteleAsync(LocalContainer container, string route);
    Task<string> EditFileAsync(IFormFile file, LocalContainer container, string route);
    Task<string> UploadAsync(IFormFile file, LocalContainer container, string route);
}