
using MediatR;

using MyApp.Application.Common.Exceptions;
using MyApp.Application.Common.Interfaces;
using MyApp.Application.ManageCategoriesFeature.Commands.CreateCommand;
using MyApp.Domain.Entities;

namespace MyApp.Application.ManageCategoriesFeature.Commands.UpdateCommand;

public class UpdateCategoryCommandHandler
    : IRequestHandler<UpdateCategoryCommand>
{
    private readonly IApplicationDbContext _dbContext;

    public UpdateCategoryCommandHandler(IApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }


    #region IRequestHandler members

    public async Task Handle(
        UpdateCategoryCommand request, 
        CancellationToken cancellationToken)
    {
        var entity
            = await _dbContext.Categories.FindAsync(keyValues: [request.CategoryId], cancellationToken)
              ?? throw new MyNotFoundException(request.CategoryId.ToString(), "Category");

        entity.CategoryName = request.CategoryName ?? string.Empty;

        await _dbContext.SaveChangesAsync(cancellationToken);
    }

    #endregion

}
