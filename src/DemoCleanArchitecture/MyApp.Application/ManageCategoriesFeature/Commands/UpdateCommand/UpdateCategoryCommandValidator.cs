using Microsoft.EntityFrameworkCore;

using MyApp.Application.Common.Interfaces;


namespace MyApp.Application.ManageCategoriesFeature.Commands.UpdateCommand;


public class UpdateCategoryCommandValidator
    : AbstractValidator<UpdateCategoryCommand>
{

    private readonly IApplicationDbContext _dbContext;

    // USEFUL FOR TESTING
    public const string ErrorCode4DuplicateName = "UNIQUE_NAME";
    public const string ErrorMsg4CategoryNameLength = "Category Name cannot have more than 50 characters";
    public const string ErrorMsg4DuplicateCategoryName = "The specified Category Name already exists.";
    public const string ErrorMsg4NullOrEmptyCategoryName = "'Category Name' must not be empty.";


    public UpdateCategoryCommandValidator(
        IApplicationDbContext dbContext)
    {
        _dbContext = dbContext;

        RuleFor(v => v.CategoryName)
            .MaximumLength(50)
                .WithMessage(ErrorMsg4CategoryNameLength)
            .NotEmpty()
                .WithErrorCode(ErrorMsg4NullOrEmptyCategoryName);

        RuleFor(v => v)
            .MustAsync(BeUniqueName)
                .WithMessage(ErrorMsg4DuplicateCategoryName)
                .WithErrorCode(ErrorCode4DuplicateName);
    }


    public async Task<bool> BeUniqueName( 
        UpdateCategoryCommand cmd, 
        CancellationToken cancellationToken)
    {
        var isFound 
            = await _dbContext.Categories
                .AnyAsync(e => e.CategoryId != cmd.CategoryId 
                               && e.CategoryName == cmd.CategoryName, cancellationToken);

        return isFound == false;
    }

}
