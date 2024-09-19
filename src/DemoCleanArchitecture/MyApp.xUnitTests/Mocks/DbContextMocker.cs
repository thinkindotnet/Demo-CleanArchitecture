using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

using Moq;

using MyApp.Application.Common.Interfaces;
using MyApp.Infrastructure.Data;
using MyApp.Infrastructure.Interceptors;


namespace MyApp.xUnitTests.Mocks;


/// <remarks>
///     Typically, EF creates a single IServiceProvider for all contexts of a given type in an AppDomain.
///     Meaning, all context instances would share the same InMemory database instance.
///     So, ensure that each test method gets its own locally scoped InMemory database.
/// </remarks>

public class DbContextMocker
    : IDisposable
{

    private ApplicationDbContext? _dbContext;


    public ApplicationDbContext GetApplicationDbContext(string databaseName)
    {
        // Create a fresh service provider, therefore a fresh InMemory database instance.
        var serviceProvider = new ServiceCollection()
             .AddEntityFrameworkInMemoryDatabase()
             .BuildServiceProvider();

        // Create a new options instance,
        // telling the context to use InMemory database and the new service provider.
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

        // Create the instance of the DbContext (would be an InMemory database)
        // NOTE: It will use the Schema as defined in the Data (in Infrastructure) and Entities (in Domain) folders.
        _dbContext = new ApplicationDbContext(options, interceptor);

        // Seed the inmemory database.
        var initializer = new ApplicationDbContextInitializer(_dbContext);
        initializer.Initialize();
        initializer.Seed();

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
