using MyApp.Application.Common.Exceptions;
using MyApp.Application.ManageCategoriesFeature.Commands.CreateCommand;
using MyApp.Application.ManageCategoriesFeature.Commands.DeleteCommand;
using MyApp.xUnitTests.Common;

using Xunit.Abstractions;


namespace MyApp.xUnitTests.CategoryTests.Commands;


public class Tests4CreateCommand
    : MyTestFixture
{

    private readonly ITestOutputHelper _outputHelper;


    public Tests4CreateCommand(
        ITestOutputHelper outputHelper)
    {
        _outputHelper = outputHelper;
    }


    [Fact]
    public async Task ShouldCreateCategory()
    {
        // Arrange
        var command = new CreateCategoryCommand
        {
            CategoryName = "New Category"
        };
        var handler = new CreateCategoryCommandHandler(DbContext);

        // Act
        var id = await handler.Handle(command, CancellationToken.None);
        _outputHelper.WriteLine("-- inserted.");
        var entity = await DbContext.Categories.FindAsync(id);

        // Assert - 1
        entity.Should().NotBeNull();
        _outputHelper.WriteLine("-- retrieved the inserted object");

        // Assert - 2
        entity?.CategoryId.Should().Be(id);
        _outputHelper.WriteLine("-- CategoryId of the inserted object matches with the Response of the InsertCommand");

        // Assert - 2
        entity?.CategoryName.Should().BeEquivalentTo(command.CategoryName);
        _outputHelper.WriteLine("-- CategoryName of the inserted object matches with the value submitted.");

        _outputHelper.WriteLine(string.Empty);
        _outputHelper.WriteLine("Completed {0} successfully.", nameof(ShouldCreateCategory));
    }

}
