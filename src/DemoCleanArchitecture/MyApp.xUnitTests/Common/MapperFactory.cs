using MyApp.Application.Common.Mappings;


namespace MyApp.xUnitTests.Common;


public static class MapperFactory
{
    public static IMapper Create()
    {
        var configurationProvider = new MapperConfiguration(config =>
        {
            config.AddProfile<MappingProfile>();
        });

        return configurationProvider.CreateMapper();
    }
}
