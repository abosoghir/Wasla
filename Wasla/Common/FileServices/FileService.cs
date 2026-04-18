using Wasla.Common.ResultPattern;

namespace Wasla.Common.FileServices;

public class FileService(IWebHostEnvironment env, ILogger<FileService> logger) : IFileService
{
    private readonly ILogger<FileService> _logger = logger;

    public async Task<Result<string?>> SaveFileAsync(
        IFormFile? file,
        string folder,
        string[] allowedExtensions,
        long maxBytes)
    {
        if (file is null || file.Length == 0)
            return Result.Success<string?>(null);

        var extension = Path.GetExtension(file.FileName).ToLowerInvariant();

        if (!allowedExtensions.Contains(extension))
            return Result.Failure<string?>(FileErrors.InvalidExtension);

        if (file.Length > maxBytes)
            return Result.Failure<string?>(FileErrors.MaxSizeExceeded);

        var normalizedFolder = folder.Trim().TrimStart('/')
            .Replace('\\', '/');

        var physicalFolder = Path.Combine(
            env.WebRootPath,
            normalizedFolder.Replace('/', Path.DirectorySeparatorChar)
        );

        Directory.CreateDirectory(physicalFolder);

        var fileName = $"{Guid.NewGuid()}{extension}";
        var physicalPath = Path.Combine(physicalFolder, fileName);

        await using var stream = new FileStream(
            physicalPath,
            FileMode.Create,
            FileAccess.Write,
            FileShare.None,
            bufferSize: 81920,
            useAsync: true
        );

        await file.CopyToAsync(stream);

        var relativePath = $"{normalizedFolder}/{fileName}".Replace("\\", "/");

        return Result.Success<string?>(relativePath);
    }

    public async Task DeleteFileAsync(string? relativePath)
    {
        if (string.IsNullOrWhiteSpace(relativePath))
            return;

        var normalizedPath = relativePath
            .Trim()
            .TrimStart('/')
            .Replace('\\', '/');

        var physicalPath = Path.Combine(
            env.WebRootPath,
            normalizedPath.Replace('/', Path.DirectorySeparatorChar)
        );

        var fullPhysicalPath = Path.GetFullPath(physicalPath);
        var fullWebRootPath = Path.GetFullPath(env.WebRootPath);

        if (!fullPhysicalPath.StartsWith(fullWebRootPath))
            return;

        if (!File.Exists(fullPhysicalPath))
            return;

        try
        {
            await Task.Run(() => File.Delete(fullPhysicalPath));
        }
        catch (IOException ex)
        {
            _logger.LogWarning(
                ex,
                "IO error while deleting file. Path: {FilePath}",
                fullPhysicalPath
            );
        }
        catch (UnauthorizedAccessException ex)
        {
            _logger.LogWarning(
                ex,
                "Unauthorized access while deleting file. Path: {FilePath}",
                fullPhysicalPath
            );
        }
    }

}
