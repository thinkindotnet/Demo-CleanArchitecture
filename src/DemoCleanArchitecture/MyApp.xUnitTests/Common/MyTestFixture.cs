using AutoMapper;

using MyApp.Application.Common.Mappings;
using MyApp.Infrastructure.Data;
using MyApp.xUnitTests.Mocks;

namespace MyApp.xUnitTests.Common;

public class MyTestFixture
    : IDisposable
{

    private readonly DbContextMocker? _dbContextMocker;

    public ApplicationDbContext DbContext { get; }

    public IMapper Mapper { get; }


    public MyTestFixture()
    {
        var dbName = Guid.NewGuid().ToString();
        _dbContextMocker = new DbContextMocker();
        DbContext = _dbContextMocker.GetApplicationDbContext(dbName);

        Mapper = MapperFactory.Create();
    }


    #region IDisposable members

    public void Dispose()
    {
        _dbContextMocker?.Dispose();
        GC.SuppressFinalize(this);
    }

    #endregion

}
