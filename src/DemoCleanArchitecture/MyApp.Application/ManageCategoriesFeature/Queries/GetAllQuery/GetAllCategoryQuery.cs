using MediatR;

using MyApp.Application.ManageCategoriesFeature.DTOs;

namespace MyApp.Application.ManageCategoriesFeature.Queries.GetAllQuery;

public class GetAllCategoryQuery
    : IRequest<AllCategoriesDto>
{

}
