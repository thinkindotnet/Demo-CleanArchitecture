using MyApp.Application.ManageCategoriesFeature.Commands.CreateCommand;
using MyApp.xUnitTests.Common;

using FluentValidation.TestHelper;
using Xunit.Abstractions;


namespace MyApp.xUnitTests.CategoryTests.Commands;


public class Tests4CreateCommandValidators
    : MyTestFixture
{

    private readonly ITestOutputHelper _outputHelper;


    public Tests4CreateCommandValidators(
        ITestOutputHelper outputHelper)
    {
        _outputHelper = outputHelper;
    }


    [Fact]
    public async Task IsValid_True_WhenCategoryNameIsUnique()
    {
        // Arrange
        var command = new CreateCategoryCommand
        {
            CategoryName = "New Category"               // does not exist in the Seed Data.
        };
        var validator = new CreateCategoryCommandValidator(DbContext);

        // Act
        var result = await validator.TestValidateAsync(command);

        // Assert
        result.IsValid.Should().BeTrue();
        _outputHelper.WriteLine("CategoryName of the new Category is Unique.");

        _outputHelper.WriteLine(string.Empty);
        _outputHelper.WriteLine("Completed successfully.");
    }


    [Fact]
    public async Task IsValid_False_WhenCategoryNameIsNotUnique()
    {
        // Arrange
        var command = new CreateCategoryCommand
        {
            CategoryName = "Fourth Category"            // exists in the Seed Data.
        };
        var validator = new CreateCategoryCommandValidator(DbContext);

        // Act
        var result = await validator.TestValidateAsync(command);

        // Assert
        result
            .ShouldHaveValidationErrorFor(e => e.CategoryName)
            .WithErrorCode(CreateCategoryCommandValidator.ErrorCode4DuplicateName)
            .WithErrorMessage(CreateCategoryCommandValidator.ErrorMsg4DuplicateCategoryName);
        _outputHelper.WriteLine("Cannot Insert Category with duplicate Category Name.");

        _outputHelper.WriteLine(string.Empty);
        _outputHelper.WriteLine("Completed successfully.");
    }



    [Fact]
    public async Task IsValid_False_WhenCategoryNameIsNull()
    {
        // Arrange
        var command = new CreateCategoryCommand
        {
            CategoryName = null
        };
        var validator = new CreateCategoryCommandValidator(DbContext);

        // Act
        var result = await validator.TestValidateAsync(command);

        // Assert
        result
            .ShouldHaveValidationErrorFor(e => e.CategoryName)
            .WithErrorMessage(CreateCategoryCommandValidator.ErrorMsg4NullOrEmptyCategoryName);
        _outputHelper.WriteLine("Cannot Insert Category with NULL Category Name.");

        _outputHelper.WriteLine(string.Empty);
        _outputHelper.WriteLine("Completed successfully.");
    }


    [Fact]
    public async Task IsValid_False_WhenCategoryNameIsEmpty()
    {
        // Arrange
        var command = new CreateCategoryCommand
        {
            CategoryName = string.Empty
        };
        var validator = new CreateCategoryCommandValidator(DbContext);

        // Act
        var result = await validator.TestValidateAsync(command);

        // Assert
        result
            .ShouldHaveValidationErrorFor(e => e.CategoryName)
            .WithErrorMessage(CreateCategoryCommandValidator.ErrorMsg4NullOrEmptyCategoryName);
        _outputHelper.WriteLine("Cannot Insert Category with EMPTY Category Name.");

        _outputHelper.WriteLine(string.Empty);
        _outputHelper.WriteLine("Completed successfully.");
    }


    [Fact]
    public async Task IsValid_False_WhenCategoryNameLengthGreaterThan50()
    {
        // Arrange
        var command = new CreateCategoryCommand
        {
            CategoryName = "123456789012345678901234567890123456789012345678901"
        };
        var validator = new CreateCategoryCommandValidator(DbContext);

        // Act
        var result = await validator.TestValidateAsync(command);

        // Assert
        result
            .ShouldHaveValidationErrorFor(e => e.CategoryName)
            .WithErrorMessage(CreateCategoryCommandValidator.ErrorMsg4CategoryNameLength);
        _outputHelper.WriteLine("Cannot Insert Category with Category Name greater than 50 characters.");

        _outputHelper.WriteLine(string.Empty);
        _outputHelper.WriteLine("Completed successfully.");
    }

}
