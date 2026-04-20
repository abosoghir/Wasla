namespace Wasla.Features.Categories.GetCategories;

public class GetCategoriesHandler : IRequestHandler<GetCategoriesRequest, Result<GetCategoriesResponse>>
{
    public Task<Result<GetCategoriesResponse>> Handle(GetCategoriesRequest request, CancellationToken ct)
    {
        var taskCategories = Enum.GetValues<TaskCategory>()
            .Select(c => new CategoryDto((int)c, c.ToString()))
            .ToList();

        var serviceCategories = Enum.GetValues<ServiceCategory>()
            .Select(c => new CategoryDto((int)c, c.ToString()))
            .ToList();

        var response = new GetCategoriesResponse(taskCategories, serviceCategories);
        return Task.FromResult(Result.Success(response));
    }
}
