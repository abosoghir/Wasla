using EduBrain.Common.ResultPattern;

namespace EduBrain.Common.FileServices;

public static class FileErrors
{
    public static readonly Error NullFile =
       new("File.Null", "File is required.", StatusCodes.Status400BadRequest);

    public static readonly Error EmptyFile =
        new("File.Empty", "File is empty.", StatusCodes.Status400BadRequest);

    public static readonly Error InvalidExtension =
        new("File.InvalidExtension", "File extension is not supported.", StatusCodes.Status415UnsupportedMediaType);

    public static readonly Error MaxSizeExceeded =
        new("File.MaxSize", "Maximum allowed file size exceeded.", StatusCodes.Status413PayloadTooLarge);
}
