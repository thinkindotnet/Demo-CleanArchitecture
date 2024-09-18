using MediatR;

namespace MyApp.Application.ManageCategoriesFeature.Commands.CreateCommand;

public class CreateCategoryCommand
    : IRequest<int>                         // new Category's CategoryId
{
    public string? CategoryName { get; set; }
}
