namespace Wasla.Features.HelperProfileManagement.Skills.AddSkillToHelper;

public record AddSkillToHelperRequest(
    int SkillId,
    ProficiencyLevel Proficiency
) : IRequest<Result<AddSkillToHelperResponse>>;

public class AddSkillToHelperRequestValidator : AbstractValidator<AddSkillToHelperRequest>
{
    public AddSkillToHelperRequestValidator()
    {
        RuleFor(x => x.SkillId).GreaterThan(0);
        RuleFor(x => x.Proficiency).IsInEnum();
    }
}
