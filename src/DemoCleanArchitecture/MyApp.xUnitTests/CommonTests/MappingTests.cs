using MyApp.Application.ManageCategoriesFeature.DTOs;
using MyApp.Domain.Entities;
using MyApp.xUnitTests.Common;


namespace MyApp.xUnitTests.CommonTests;


// Integration Test between the Domain Models and Application Models

public class MappingTests
    : IClassFixture<MappingFixture>
{

    private readonly IMapper _mapper;


    public MappingTests(MappingFixture fixture)
    {
        _mapper = fixture.Mapper;
    }


    [Fact]
    public void ShouldHaveValidConfiguration()
    {
        // Arrange

        // Act

        // Assert
        _mapper
            .ConfigurationProvider
            .AssertConfigurationIsValid();
    }


    [Theory]
    [InlineData(typeof(Category), typeof(CategoryDto))]
    // [InlineData(typeof(Product), typeof(ProductDto))]
    public void ShoudSupportMappingFromSourceToDestination(Type source, Type destination)
    {
        // Arrange
        var instanceOfSource = Activator.CreateInstance(source);

        // Act

        // Assert if the Source Type members can be mapped to the Destination Type members.
        _mapper.Map(instanceOfSource, source, destination);
    }

}
