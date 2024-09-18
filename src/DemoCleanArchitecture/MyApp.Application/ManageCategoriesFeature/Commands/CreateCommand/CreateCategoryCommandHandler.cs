
using MediatR;

using MyApp.Application.Common.Interfaces;
using MyApp.Domain.Entities;

namespace MyApp.Application.ManageCategoriesFeature.Commands.CreateCommand;

public class CreateCategoryCommandHandler
    : IRequestHandler<CreateCategoryCommand, int>
{

    private readonly IApplicationDbContext _dbContext;

    public CreateCategoryCommandHandler(IApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }


    #region IRequestHandler members

    public async Task<int> Handle(
        CreateCategoryCommand request, 
        CancellationToken cancellationToken)
    {
        var entity = new Category
        {
            CategoryName = request.CategoryName ?? string.Empty
        };
        _dbContext.Categories.Add(entity);
        await _dbContext.SaveChangesAsync(cancellationToken);

        return entity.CategoryId;
    }

    #endregion

}
