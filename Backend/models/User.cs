using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class User
{
    public int UserId { get; set; }
    public required string UserName { get; set; }

    //Spørgsmål 1: 
    public required string Password { get; set; }

    public List<Card> Cards { get; } = new();
}

public class UserEntityTypeConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
    }
}