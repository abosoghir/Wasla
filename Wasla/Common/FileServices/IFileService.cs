using EduBrain.Common.ResultPattern;

namespace EduBrain.Common.FileServices;

public interface IFileService
{
    // Save file to <wwwroot>/<folder> based on allowed extensions
    Task<Result<string?>> SaveFileAsync(IFormFile? file, string folder, string[] allowedExtensions, long maxBytes);

    // Delete a previously saved file given its relative path
    Task DeleteFileAsync(string? relativePath);
}
