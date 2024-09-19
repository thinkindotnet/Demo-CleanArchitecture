using MyApp.Application.Common.Exceptions;
using MyApp.Application.ManageCategoriesFeature.Commands.UpdateCommand;
using MyApp.xUnitTests.Common;

using Xunit.Abstractions;


namespace MyApp.xUnitTests.CategoryTests.Commands;


public class Tests4UpdateCommand
    : MyTestFixture
{

    private readonly ITestOutputHelper _outputHelper;


    public Tests4UpdateCommand(
        ITestOutputHelper outputHelper)
    {
        _outputHelper = outputHelper;
    }


    [Fact]
    public async Task Should_UpdateCategory()
    {
        // Arrange
        var command = new UpdateCategoryCommand
        {
            CategoryId = 2,
            CategoryName = "Updated Second Category"
        };
        var handler = new UpdateCategoryCommandHandler(DbContext);

        // Act
        await handler.Handle(command, CancellationToken.None);
        _outputHelper.WriteLine("-- updated second category.");
        var entity = await DbContext.Categories.FindAsync(command.CategoryId);

        // Assert - 1
        entity.Should().NotBeNull();
        _outputHelper.WriteLine("-- retrieved the updated object");

        // Assert - 2
        entity?.CategoryId.Should().Be(command.CategoryId);
        entity?.CategoryName.Should().BeEquivalentTo(command.CategoryName);
        _outputHelper.WriteLine("-- Properties matches with the updated object");

        _outputHelper.WriteLine(string.Empty);
        _outputHelper.WriteLine("Completed successfully.");
    }


    [Fact]
    public async Task Should_Not_UpdateNonExistantCategory()
    {
        // Arrange
        var command = new UpdateCategoryCommand
        {
            CategoryId = 9,                             // does not exist in test data.
            CategoryName = "Changed Category"
        };
        var handler = new UpdateCategoryCommandHandler(DbContext);

        // Act
        var result 
            = await Assert.ThrowsAsync<MyNotFoundException>(async () => await handler.Handle(command, CancellationToken.None));

        _outputHelper.WriteLine("-- attempted to update on a non-existant category.");

        // Assert
        result.Message.Should().BeEquivalentTo("Queried object Category was not found.  Key: 9");

        _outputHelper.WriteLine(string.Empty);
        _outputHelper.WriteLine("Completed successfully.");
    }


    [Fact]
    public async Task Should_UpdateCategoryWithNoChanges()
    {
        // Arrange
        var command = new UpdateCategoryCommand
        {
            CategoryId = 2,                                 // no changes
            CategoryName = "Second Category"                // no changes
        };
        var handler = new UpdateCategoryCommandHandler(DbContext);

        // Act
        await handler.Handle(command, CancellationToken.None);
        _outputHelper.WriteLine("-- updated second category.");
        var entity = await DbContext.Categories.FindAsync(command.CategoryId);

        // Assert - 1
        entity.Should().NotBeNull();
        _outputHelper.WriteLine("-- retrieved the updated object");

        // Assert - 2
        entity?.CategoryId.Should().Be(command.CategoryId);
        entity?.CategoryName.Should().BeEquivalentTo(command.CategoryName);
        _outputHelper.WriteLine("-- Properties matches with the updated object");

        _outputHelper.WriteLine(string.Empty);
        _outputHelper.WriteLine("Completed successfully.");
    }

}
