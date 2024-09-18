using MediatR;

using MyApp.Application.ManageCategoriesFeature.DTOs;

namespace MyApp.Application.ManageCategoriesFeature.Queries.GetByIdQuery;

public class GetCategoryByIdQuery
    : IRequest<CategoryDto>
{

    public int CategoryId { get; set; }

}
