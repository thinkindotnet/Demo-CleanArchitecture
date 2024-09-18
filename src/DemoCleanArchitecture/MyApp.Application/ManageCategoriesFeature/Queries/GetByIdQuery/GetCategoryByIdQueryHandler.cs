
using AutoMapper;

using MediatR;

using MyApp.Application.Common.Exceptions;
using MyApp.Application.Common.Interfaces;
using MyApp.Application.ManageCategoriesFeature.DTOs;

namespace MyApp.Application.ManageCategoriesFeature.Queries.GetByIdQuery;

public class GetCategoryByIdQueryHandler
    : IRequestHandler<GetCategoryByIdQuery, CategoryDto>
{
    public readonly IApplicationDbContext _dbContext;
    public readonly IMapper _mapper;


    public GetCategoryByIdQueryHandler(
        IApplicationDbContext context, 
        IMapper mapper)
    {
        _dbContext = context;
        _mapper = mapper;
    }


    #region IRequestHandler members

    public async Task<CategoryDto> Handle(GetCategoryByIdQuery request, CancellationToken cancellationToken)
    {
        var entity 
            = await _dbContext.Categories
                              .FindAsync(keyValues: [request.CategoryId], cancellationToken)
              ?? throw new MyNotFoundException(request.CategoryId.ToString(), "Category");

        return _mapper.Map<CategoryDto>(entity);
    }

    #endregion
}
