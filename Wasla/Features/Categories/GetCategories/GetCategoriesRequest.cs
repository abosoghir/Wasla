namespace Wasla.Features.Categories.GetCategories;

public record GetCategoriesRequest() : IRequest<Result<GetCategoriesResponse>>;

public record GetCategoriesResponse(List<CategoryDto> TaskCategories, List<CategoryDto> ServiceCategories);

public record CategoryDto(int Id, string Name);
