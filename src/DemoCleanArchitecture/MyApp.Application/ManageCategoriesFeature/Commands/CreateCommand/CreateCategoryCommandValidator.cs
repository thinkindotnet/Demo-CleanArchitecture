using FluentValidation;

using MyApp.Application.Common.Interfaces;

namespace MyApp.Application.ManageCategoriesFeature.Commands.CreateCommand;

public class CreateCategoryCommandValidator
    : AbstractValidator<CreateCategoryCommand>
{

    private readonly IApplicationDbContext _dbContext;

    // USEFUL FOR TESTING
    public const string ErrorCode4DuplicateName = "UNIQUE_NAME";
    public const string ErrorMsg4DuplicateCategoryName = "The specified Category Name already exists.";
    public const string ErrorMsg4NullOrEmptyCategoryName = "Category Name cannot be empty";

    public CreateCategoryCommandValidator(
        IApplicationDbContext dbContext)
    {
        _dbContext = dbContext;

        RuleFor(v => v.CategoryName)
            .MaximumLength(50)
                .WithMessage("Category Name cannot have more than 50 characters")
            .NotEmpty()
                .WithErrorCode(ErrorMsg4NullOrEmptyCategoryName)
            .MustAsync(BeUniqueName)
                .WithMessage(ErrorMsg4DuplicateCategoryName)
                .WithErrorCode(ErrorCode4DuplicateName);
    }


    public async Task<bool> BeUniqueName(string name, CancellationToken cancellationToken)
    {
        return await _dbContext.Categories.AllAsync(e => e.CategoryName != name, cancellationToken);
    }
}
