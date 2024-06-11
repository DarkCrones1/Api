using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Hosting;

using Api.Common.Enumerations;
using Api.Common.Interfaces.Repositories;
using Microsoft.AspNetCore.Routing;
using Microsoft.IdentityModel.Tokens;

namespace AW.Infrastructure.Repositories;

public class LocalStorageRepository : ILocalStorageRepository
{
    private readonly IConfiguration _configuration;
    private readonly IWebHostEnvironment _env;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public LocalStorageRepository(IConfiguration configuration, IWebHostEnvironment env, IHttpContextAccessor httpContextAccessor)
    {
        this._configuration = configuration;
        this._env = env;
        this._httpContextAccessor = httpContextAccessor;
    }

    public Task DeteleAsync(LocalContainer container, string route)
    {
        var containerName = Enum.GetName(typeof(LocalContainer), container)!.ToLower();
        containerName = containerName.Replace("_", "-");

        if (route != null)
        {
            var fileName = Path.GetFileName(route);
            string directory = Path.Combine(_env.WebRootPath, containerName, fileName);

            if (File.Exists(directory))
            {
                File.Delete(directory);
            }
        }
        return Task.FromResult("Archivo Eliminado");
    }

    public async Task<string> EditFileAsync(IFormFile file, LocalContainer container, string route)
    {
        await DeteleAsync(container, route);
        return await UploadAsync(file, container, route);
    }

    public async Task<string> UploadAsync(IFormFile file, LocalContainer container, string route)
    {
        if (file.Length == 0) return null!;

        // Nombre del contenedor
        var containerName = Enum.GetName(typeof(LocalContainer), container)!.ToLower();
        containerName = containerName.Replace("_", "-");

        // Ruta de la carpeta
        var folder = Path.Combine(_env.WebRootPath, containerName);

        // Si no existe la carpeta, la crea
        if (!Directory.Exists(folder))
        {
            Directory.CreateDirectory(folder);
        }

        // Agrega al nombre de archivo una extensión
        route = $"{Guid.NewGuid()}{Path.GetExtension(file.FileName)}";

        // Ruta de la iamgen
        var filePath = Path.Combine(folder, route);

        using (var memoryStream = new MemoryStream())
        {
            await file.CopyToAsync(memoryStream);
            await File.WriteAllBytesAsync(filePath, memoryStream.ToArray());
        }

        var urlActually = $"{_httpContextAccessor.HttpContext!.Request.Scheme}://{_httpContextAccessor.HttpContext.Request.Host}";

        var urlDB = Path.Combine(urlActually, containerName, route).Replace("\\", "/");
        return urlDB;
    }
}