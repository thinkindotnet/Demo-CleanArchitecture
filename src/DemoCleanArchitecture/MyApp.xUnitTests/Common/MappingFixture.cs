using AutoMapper;

namespace MyApp.xUnitTests.Common;

public class MappingFixture
{

    public IMapper Mapper { get; set; }

    public MappingFixture() 
    {
        Mapper = MapperFactory.Create();
    }

}
