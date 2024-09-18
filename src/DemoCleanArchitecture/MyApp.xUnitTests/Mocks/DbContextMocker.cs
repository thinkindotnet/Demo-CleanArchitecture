using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

using Moq;

using MyApp.Application.Common.Interfaces;
using MyApp.Infrastructure.Data;
using MyApp.Infrastructure.Data.Interceptors;

namespace MyApp.xUnitTests.Mocks;

public class DbContextMocker
    : IDisposable
{

    private ApplicationDbContext? _dbContext;


    public ApplicationDbContext GetApplicationDbContext(string databaseName)
    {
        var serviceProvider = new ServiceCollection()
             .AddEntityFrameworkInMemoryDatabase()
             .BuildServiceProvider();

        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(databaseName)
            .UseInternalServiceProvider(serviceProvider)
            .Options;

        // Mock of the CurrentUser Service.
        var currentUserServiceMock = new Mock<ICurrentUserService>();
        currentUserServiceMock
            .Setup(m => m.UserId)
            .Returns(Guid.Empty.ToString());

        // Grab the AuditableEntity SaveChanges Interceptor
        var interceptor = new AuditableEntitySaveChangesInterceptor(currentUserServiceMock.Object);


        _dbContext = new ApplicationDbContext(options, interceptor);


        // Seed the inmemory database.
        var initializer = new ApplicationDbContextInitializer(_dbContext);
        initializer.Initialize();


        return _dbContext;
    }


    #region IDisposable members

    public void Dispose()
    {
        _dbContext?.Dispose();

        GC.SuppressFinalize(this);
    }

    #endregion

}
