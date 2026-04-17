namespace EduBrain.Common.Consts;


public static class RegexPatterns
{
    //public const string Password = "(?=(.*[0-9]))(?=.*[\\!@#$%^&*()\\\\[\\]{}\\-_+=~`|:;\"'<>,./?])(?=.*[a-z])(?=(.*[A-Z]))(?=(.*)).{8,}";
    public const string Password = @"(?=.*[0-9])(?=.*[!@#$%^&*()\[\]{}\-_+=~`|:;""'<>,./?]).{6,}";

    // Egyptian phone number (11 digits, starts with 010, 011, 012, or 015)
    public const string PhoneNumber = @"^(010|011|012|015)\d{8}$";
}
