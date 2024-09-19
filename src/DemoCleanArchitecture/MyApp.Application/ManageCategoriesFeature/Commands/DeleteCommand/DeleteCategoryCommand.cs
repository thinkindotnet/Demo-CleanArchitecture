namespace MyApp.Application.ManageCategoriesFeature.Commands.DeleteCommand;


public class DeleteCategoryCommand
    : IRequest                         
{

    public int CategoryId { get; set; }

}
