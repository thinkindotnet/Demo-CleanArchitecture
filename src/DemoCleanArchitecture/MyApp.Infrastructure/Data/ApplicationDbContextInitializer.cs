

using Microsoft.EntityFrameworkCore;

namespace MyApp.Infrastructure.Data;

public class ApplicationDbContextInitializer
{

    private readonly ApplicationDbContext _dbContext;


    public ApplicationDbContextInitializer(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }


    public void Initialize()
    {
        _dbContext.Database.EnsureCreated();

        if (_dbContext.Database.IsSqlServer())
        {
            _dbContext.Database.Migrate();
        }

    }
}
