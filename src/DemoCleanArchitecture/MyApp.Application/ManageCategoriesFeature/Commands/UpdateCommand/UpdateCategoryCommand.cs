using MediatR;

namespace MyApp.Application.ManageCategoriesFeature.Commands.UpdateCommand;

public class UpdateCategoryCommand
    : IRequest                         
{
    public int CategoryId { get; set; }

    public string? CategoryName { get; set; }

}
