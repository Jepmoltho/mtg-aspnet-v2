/*
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;

public class MtgContext : DbContext
{
    public DbSet<User> Users { get; set; }
    public DbSet<Card> Cards { get; set; }

    // public string DbPath { get; }

    public MtgContext(DbContextOptions<MtgContext>options) : base(options)
    {
        //dotnet ef migrations add <name>

        // var folder = Environment.SpecialFolder.LocalApplicationData;
        // var path = Environment.GetFolderPath(folder);
        // DbPath = System.IO.Path.Join(path, "Userging.db");
    }

    // The following configures EF to create a Sqlite database file in the
    // special "local" folder for your platform.
    // protected override void OnConfiguring(DbContextOptionsBuilder options)
    //     => options.UseSqlServer($"Data Source=.,1433;Initial Catalog=Mtg;TrustServerCertificate=True;");
        
}

public class User
{
    public int UserId { get; set; }
    public string UserName { get; set; }

    public List<Card> Cards { get; } = new();
}

public class Card
{
    public int CardId { get; set; }
    public string Title { get; set; }
    //public string Set { get; set; }

    public int UserId { get; set; }
    public User User { get; set; }
}
*/
// using Microsoft.EntityFrameworkCore;
// using System;
// using System.Collections.Generic;

// public class MtgContext : DbContext
// {
//     public DbSet<User> Users { get; set; }
//     public DbSet<Card> Cards { get; set; }

//     public string DbPath { get; }

//     public MtgContext()
//     {
//         var folder = Environment.SpecialFolder.LocalApplicationData;
//         var path = Environment.GetFolderPath(folder);
//         DbPath = System.IO.Path.Join(path, "Userging.db");
//     }

//     // The following configures EF to create a Sqlite database file in the
//     // special "local" folder for your platform.
//     protected override void OnConfiguring(DbContextOptionsBuilder options)
//         => options.UseSqlServer($"Data Source={DbPath}");
// }

// public class User
// {
//     public int UserId { get; set; }
//     public string UserName { get; set; }

//     public List<Card> Cards { get; } = new();
// }

// public class Card
// {
//     public int CardId { get; set; }
//     public string Title { get; set; }
//     public string Content { get; set; }

//     public int UserId { get; set; }
//     public User User { get; set; }
// }
