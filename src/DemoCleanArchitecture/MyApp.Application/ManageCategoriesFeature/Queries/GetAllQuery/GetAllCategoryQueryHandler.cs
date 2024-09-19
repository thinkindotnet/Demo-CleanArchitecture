using AutoMapper.QueryableExtensions;

using MyApp.Application.Common.Interfaces;
using MyApp.Application.ManageCategoriesFeature.DTOs;


namespace MyApp.Application.ManageCategoriesFeature.Queries.GetAllQuery;


public class GetAllCategoryQueryHandler
    : IRequestHandler<GetAllCategoryQuery, AllCategoriesDto>
{

    private readonly IApplicationDbContext _dbContext;
    private readonly IMapper _mapper;


    public GetAllCategoryQueryHandler(
        IApplicationDbContext dbContext, 
        IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }
    

    #region IRequestHandler members

    public async Task<AllCategoriesDto> Handle(
        GetAllCategoryQuery request, 
        CancellationToken cancellationToken)
    {
        return new AllCategoriesDto
        {
            Categories = await _dbContext.Categories
                                         .ProjectTo<CategoryDto>(_mapper.ConfigurationProvider)
                                         .ToListAsync(cancellationToken)
        };
    }

    #endregion

}
