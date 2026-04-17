namespace EduBrain.Common.ResultPattern;

public record Error(string code, string Description, int? StatusCode)
{
    public static readonly Error None = new(string.Empty, string.Empty, null);
}
