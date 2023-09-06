using Microsoft.EntityFrameworkCore;

/// <summary>
/// dotnet ef migrations add <name of the migration>
/// Shut down the backend when running migrations
/// The context class is a bridge between my application and the database. It inherits (:) from DbContext from the Microsoft.EntityFrameworkCore namespace
/// </summary>
public class MtgContext : DbContext
{
    public DbSet<User> Users { get; set; }
    public DbSet<Card> Cards { get; set; }

    public MtgContext(DbContextOptions<MtgContext>options) : base(options){
        Users = Set<User>(); //Added this to fix the nullable error
        Cards = Set<Card>(); //Added this to fix the nullable error
    }


}
