using MyApp.Application.Common.Mappings;
using MyApp.Domain.Entities;


namespace MyApp.Application.ManageCategoriesFeature.DTOs;


public class CategoryDto
     : IMapFrom<Category>
{

    public int CategoryId { get; set; }


    public string CategoryName { get; set; } 
        = string.Empty;

}
