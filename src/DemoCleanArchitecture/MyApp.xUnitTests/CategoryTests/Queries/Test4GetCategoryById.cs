using MyApp.Application.Common.Exceptions;
using MyApp.Application.ManageCategoriesFeature.DTOs;
using MyApp.Application.ManageCategoriesFeature.Queries.GetAllQuery;
using MyApp.Application.ManageCategoriesFeature.Queries.GetByIdQuery;
using MyApp.Infrastructure.Data;
using MyApp.xUnitTests.Common;

using Xunit.Abstractions;


namespace MyApp.xUnitTests.CategoryTests.Queries;

[Collection(nameof(MyTestQueryCollection))]
public class Test4GetCategoryById
{
    private readonly ApplicationDbContext _context;
    private readonly IMapper _mapper;
    private readonly ITestOutputHelper _outputHelper;

    public Test4GetCategoryById(
        MyTestFixture fixture,
        ITestOutputHelper outputHelper)
    {
        _context = fixture.DbContext;
        _mapper = fixture.Mapper;
        _outputHelper = outputHelper;
    }


    [Fact]
    public async Task Returns_CorrectData()
    {
        // Arrange
        int searchCategoryId = 2;
        var query = new GetCategoryByIdQuery() { CategoryId = searchCategoryId };
        var handler = new GetCategoryByIdQueryHandler(_context, _mapper);
        var expected = await _context.Categories.FindAsync(searchCategoryId);

        // Act
        var result = await handler.Handle(query, CancellationToken.None);

        // Assert - 1
        result.Should().NotBeNull();
        _outputHelper.WriteLine("-- is not null.");

        // Assert - 2
        result.Should().BeOfType<CategoryDto>();
        _outputHelper.WriteLine("-- is of the correct type.");

        // Assert - 3
        result.CategoryId.Should().Be(expected?.CategoryId);
        result.CategoryName.Should().BeEquivalentTo(expected?.CategoryName);
        _outputHelper.WriteLine("-- has the correct values, as in the database");

        _outputHelper.WriteLine(string.Empty);
        _outputHelper.WriteLine("Successfully completed!.");

    }


    [Fact]
    public async Task Should_ThrowNotFoundException()
    {
        // Arrange
        int searchCategoryId = 20;
        var query = new GetCategoryByIdQuery() { CategoryId = searchCategoryId };
        var handler = new GetCategoryByIdQueryHandler(_context, _mapper);
        var expected = await _context.Categories.FindAsync(searchCategoryId);
        var expectedMessage = $"Queried object Category was not found.  Key: {searchCategoryId}";

        // Act
        var result 
            = await Assert.ThrowsAsync<MyNotFoundException>(
                async () => await handler.Handle(query, CancellationToken.None));

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
