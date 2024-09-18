namespace MyApp.Domain.Entities;


public class Category
    : AuditableEntity
{

    public int CategoryId { get; set; }


    public string CategoryName { get; set; } = string.Empty;

}
