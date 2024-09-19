using MyApp.Application.ManageCategoriesFeature.DTOs;
using MyApp.Application.ManageCategoriesFeature.Queries.GetAllQuery;
using MyApp.Infrastructure.Data;
using MyApp.xUnitTests.Common;

using Xunit.Abstractions;


namespace MyApp.xUnitTests.CategoryTests.Queries;


[Collection(nameof(MyTestQueryCollection))]
public class Test4GetAllCategories
{

    private readonly ApplicationDbContext _context;
    private readonly IMapper _mapper;
    private readonly ITestOutputHelper _outputHelper;

    public Test4GetAllCategories(
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
        var query = new GetAllCategoryQuery();
        var handler = new GetAllCategoryQueryHandler(_context, _mapper);

        // Act
        var result = await handler.Handle(query, CancellationToken.None);

        // Assert - 1
        result.Should().NotBeNull();
        _outputHelper.WriteLine("-- is not null.");

        // Assert - 2
        result.Should().BeOfType<AllCategoriesDto>();
        _outputHelper.WriteLine("-- is of the correct type.");

        // Assert - 3
        result.Categories.Should().HaveCount(ApplicationDbContextInitializer.SeededNoOfCategories);
        _outputHelper.WriteLine("-- has the correct number of rows after database seeding.");

        _outputHelper.WriteLine(string.Empty);
        _outputHelper.WriteLine("Successfully completed : {0}!.", nameof(Returns_CorrectData));
    }

}
