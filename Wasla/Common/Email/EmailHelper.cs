using EduBrain.Entities.Users;
using Hangfire;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;

namespace EduBrain.Common.Email;

public class EmailHelper(IHttpContextAccessor httpContextAccessor, IEmailSender emailSender)
{
    private readonly IEmailSender _emailSender = emailSender;
    private readonly IHttpContextAccessor _httpContextAccessor = httpContextAccessor;

    public async Task SendConfirmationEmail(ApplicationUser user, string code)
    {
        var emailBody = EmailBodyBuilder.GenerateEmailBody("EmailConfirmation",
            templateModel: new Dictionary<string, string>
            {
            { "{{name}}", user.Name },
            { "{{code}}", code }
            }
        );

        BackgroundJob.Enqueue(() => _emailSender.SendEmailAsync(
            user.Email!,
            "🔐 EduBrain App: Email Confirmation",
            emailBody
        ));

        await Task.CompletedTask;
    }


    public async Task SendResetPasswordEmail(ApplicationUser user, string code)
    {
        var emailBody = EmailBodyBuilder.GenerateEmailBody("ForgetPassword",
            templateModel: new Dictionary<string, string>
            {
            { "{{name}}", user.Name },
            { "{{code}}", code }
            }
        );

        BackgroundJob.Enqueue(() => _emailSender.SendEmailAsync(
            user.Email!,
            "🔐 EduBrain App: Reset Password",
            emailBody
        ));

        await Task.CompletedTask;
    }

    public async Task SendPostNotificationEmailAsync(string email, string Name, string categoryTitle, string postTitle, string postLink)
    {

        var emailBody = EmailBodyBuilder.GenerateEmailBody("NewPostTemplate",
            templateModel: new Dictionary<string, string>
            {
                { "{{name}}", Name },
                { "{{categoryTitle}}", categoryTitle },
                { "{{postTitle}}", postTitle },
                { "{{postLink}}", postLink }
            }
        );

        BackgroundJob.Enqueue(() => _emailSender.SendEmailAsync(
            email,
            "New post in your favorite category!",
            emailBody
        ));

        await Task.CompletedTask;
    }

    public async Task SendFamilyMemberOtpEmail(string email, string name, string code)
    {
        var emailBody = EmailBodyBuilder.GenerateEmailBody("FamilyMemberOtp",
            templateModel: new Dictionary<string, string>
            {
                { "{{name}}", name },
                { "{{code}}", code }
            }
        );

        BackgroundJob.Enqueue(() => _emailSender.SendEmailAsync(
            email,
            "🔐 EduBrain: Family Member Verification Code",
            emailBody
        ));

        await Task.CompletedTask;
    }
}