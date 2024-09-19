using MyApp.Domain.Entities;

namespace MyApp.Infrastructure.Data;


public class ApplicationDbContextInitializer
{

    private readonly ApplicationDbContext _dbContext;

    /// <summary>
    ///     Useful for running Tests on the Seeded Data.
    /// </summary>
    public static int SeededNoOfCategories = 0;


    public ApplicationDbContextInitializer(
        ApplicationDbContext dbContext)
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


    public void Seed()
    {
        if (_dbContext.Categories.Any())
        {
            return;
        }

        var list = new Category[]
        {
            new Category { CategoryName = "First Category"},
            new Category { CategoryName = "Second Category"},
            new Category { CategoryName = "Third Category"},
            new Category { CategoryName = "Fourth Category"},
            new Category { CategoryName = "Fifth Category"},
        };
        _dbContext.Categories.AddRange(list);
        _dbContext.SaveChanges();

        SeededNoOfCategories = list.Length;
    }
}
