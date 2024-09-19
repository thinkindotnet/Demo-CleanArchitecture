using MyApp.Application.ManageCategoriesFeature.Commands.UpdateCommand;
using MyApp.xUnitTests.Common;

using FluentValidation.TestHelper;
using Xunit.Abstractions;


namespace MyApp.xUnitTests.CategoryTests.Commands;


public class Tests4UpdateCommandValidators
    : MyTestFixture
{

    private readonly ITestOutputHelper _outputHelper;


    public Tests4UpdateCommandValidators(
        ITestOutputHelper outputHelper)
    {
        _outputHelper = outputHelper;
    }


    [Fact]
    public async Task IsValid_True_WhenCategoryNameIsUnique()
    {
        // Arrange
        var command = new UpdateCategoryCommand
        {
            CategoryId = 4,
            CategoryName = "New Category"               // does not exist in the Seed Data.
        };
        var validator = new UpdateCategoryCommandValidator(DbContext);

        // Act
        var result = await validator.TestValidateAsync(command);

        // Assert
        result.IsValid.Should().BeTrue();
        _outputHelper.WriteLine("CategoryName is Unique.");

        _outputHelper.WriteLine(string.Empty);
        _outputHelper.WriteLine("Completed successfully.");
    }


    [Fact]
    public async Task IsValid_False_WhenCategoryNameIsNotUnique()
    {
        // Arrange
        var command = new UpdateCategoryCommand
        {
            CategoryId = 4,
            CategoryName = "Second Category"            // exists in the Seed Data.
        };
        var validator = new UpdateCategoryCommandValidator(DbContext);

        // Act
        var result = await validator.TestValidateAsync(command);

        // Assert
        result
            .ShouldHaveValidationErrorFor(e => e)
            .WithErrorCode(UpdateCategoryCommandValidator.ErrorCode4DuplicateName)
            .WithErrorMessage(UpdateCategoryCommandValidator.ErrorMsg4DuplicateCategoryName);
        _outputHelper.WriteLine("Cannot Update Category with duplicate Category Name.");

        _outputHelper.WriteLine(string.Empty);
        _outputHelper.WriteLine("Completed successfully.");
    }



    [Fact]
    public async Task IsValid_False_WhenCategoryNameIsNull()
    {
        // Arrange
        var command = new UpdateCategoryCommand
        {
            CategoryId = 4,
            CategoryName = null
        };
        var validator = new UpdateCategoryCommandValidator(DbContext);

        // Act
        var result = await validator.TestValidateAsync(command);

        // Assert
        result
            .ShouldHaveValidationErrorFor(e => e.CategoryName)
            .WithErrorMessage(UpdateCategoryCommandValidator.ErrorMsg4NullOrEmptyCategoryName);
        _outputHelper.WriteLine("Cannot Update Category with NULL Category Name.");

        _outputHelper.WriteLine(string.Empty);
        _outputHelper.WriteLine("Completed successfully.");
    }


    [Fact]
    public async Task IsValid_False_WhenCategoryNameIsEmpty()
    {
        // Arrange
        var command = new UpdateCategoryCommand
        {
            CategoryId = 4,
            CategoryName = string.Empty
        };
        var validator = new UpdateCategoryCommandValidator(DbContext);

        // Act
        var result = await validator.TestValidateAsync(command);

        // Assert
        result
            .ShouldHaveValidationErrorFor(e => e.CategoryName)
            .WithErrorMessage(UpdateCategoryCommandValidator.ErrorMsg4NullOrEmptyCategoryName);
        _outputHelper.WriteLine("Cannot Update Category with EMPTY Category Name.");

        _outputHelper.WriteLine(string.Empty);
        _outputHelper.WriteLine("Completed successfully.");
    }


    [Fact]
    public async Task IsValid_False_WhenCategoryNameLengthGreaterThan50()
    {
        // Arrange
        var command = new UpdateCategoryCommand
        {
            CategoryId = 4,
            CategoryName = "123456789012345678901234567890123456789012345678901"
        };
        var validator = new UpdateCategoryCommandValidator(DbContext);

        // Act
        var result = await validator.TestValidateAsync(command);

        // Assert
        result
            .ShouldHaveValidationErrorFor(e => e.CategoryName)
            .WithErrorMessage(UpdateCategoryCommandValidator.ErrorMsg4CategoryNameLength);
        _outputHelper.WriteLine("Cannot Update Category with Category Name greater than 50 characters.");

        _outputHelper.WriteLine(string.Empty);
        _outputHelper.WriteLine("Completed successfully.");
    }

}
