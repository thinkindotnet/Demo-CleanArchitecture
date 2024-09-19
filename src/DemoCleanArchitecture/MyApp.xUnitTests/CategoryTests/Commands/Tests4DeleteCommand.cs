using MyApp.Application.Common.Exceptions;
using MyApp.Application.ManageCategoriesFeature.Commands.DeleteCommand;
using MyApp.xUnitTests.Common;

using Xunit.Abstractions;


namespace MyApp.xUnitTests.CategoryTests.Commands;


public class Tests4DeleteCommand
    : MyTestFixture
{

    private readonly ITestOutputHelper _outputHelper;


    public Tests4DeleteCommand(
        ITestOutputHelper outputHelper)
    {
        _outputHelper = outputHelper;
    }


    [Fact]
    public async Task Should_DeleteCategory()
    {
        // Arrange
        var command = new DeleteCategoryCommand
        {
            CategoryId = 2
        };
        var handler = new DeleteCategoryCommandHandler(DbContext);

        // Act
        await handler.Handle(command, CancellationToken.None);
        _outputHelper.WriteLine("-- deleted second category.");

        // Assert - 1
        var entity = await DbContext.Categories.FindAsync(command.CategoryId);
        entity.Should().BeNull();
        _outputHelper.WriteLine("-- deleted object not found in database.");

        _outputHelper.WriteLine(string.Empty);
        _outputHelper.WriteLine("Completed successfully.");
    }


    [Fact]
    public async Task Should_ThrowNotFoundException()
    {
        // Arrange
        int searchCategoryId = 20;
        var command = new DeleteCategoryCommand
        {
            CategoryId = searchCategoryId
        };
        var handler = new DeleteCategoryCommandHandler(DbContext);
        var expected = await DbContext.Categories.FindAsync(searchCategoryId);
        var expectedMessage = $"Queried object Category was not found.  Key: {searchCategoryId}";

        // Act
        var result
            = await Assert.ThrowsAsync<MyNotFoundException>(
                async () => await handler.Handle(command, CancellationToken.None));

        // Assert - 1
        result.Should().NotBeNull();
        _outputHelper.WriteLine("-- returned the expected NotFoundException object.");

        // Assert - 2
        result.Message.Should().NotBeEmpty();
        _outputHelper.WriteLine("-- exception message is not empty.");

        // Assert - 3
        result.Message.Should().BeEquivalentTo(expectedMessage);
        _outputHelper.WriteLine("-- exception message is correct.");

        _outputHelper.WriteLine(string.Empty);
        _outputHelper.WriteLine("Successfully completed!.");
    }

}
