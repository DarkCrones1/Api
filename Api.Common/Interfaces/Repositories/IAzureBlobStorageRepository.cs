using Api.Common.Enumerations;
using Microsoft.AspNetCore.Http;

namespace Api.Common.Interfaces.Repositories;

public interface IAzureBlobStorageRepository
{
    /// <summary>
    /// This method uploads a file submitted with the request
    /// </summary>
    /// <param name="file">File for upload</param>
    /// <param name="container">Container where to submit the file</param>
    /// <param name="blobName">Blob name to update</param>
    /// <returns>File name saved in Blob contaienr</returns>
    Task<string> UploadAsync(IFormFile file, AzureContainer container, string blobFilename);

    /// <summary>
    /// This method deleted a file with the specified filename
    /// </summary>
    /// <param name="container">Container where to delete the file</param>
    /// <param name="blobFilename">Filename</param>
    Task DeleteAsync(AzureContainer container, string blobFilename);
}