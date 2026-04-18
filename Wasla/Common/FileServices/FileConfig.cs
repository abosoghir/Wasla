namespace Wasla.Common.FileServices;

public static class FileConfig
{
    // Allowed extensions
    public static readonly string[] ImageExtensions = [".jpg", ".jpeg", ".png", ".gif"];
    public static readonly string[] DocumentExtensions = [".pdf", ".docx", ".doc"];

    // Default folders
    public static readonly string ImagesFolderPath = "Images";
    public static readonly string DocumentsFolderPath = "Documents";

    // Maximum file sizes
    public const long MaxImageBytes = 5 * 1024 * 1024;     // 1 MB for images
    public const long MaxDocumentBytes = 20 * 1024 * 1024; // 5 MB for documents
}