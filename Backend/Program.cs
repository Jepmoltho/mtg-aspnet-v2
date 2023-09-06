using Backend.Controllers;
using Microsoft.EntityFrameworkCore;
using static Backend.Controllers.MtgController;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<MtgContext>(builder => builder.UseSqlServer("Data Source=.,1433;Initial Catalog=Mtg;TrustServerCertificate=True;User ID=sa;Password=1234Abcd;"));

var app = builder.Build();
// dotnet ef update
//app.Services.GetRequiredService<MtgContext>().Database.Migrate();
using var scope = app.Services.CreateScope();
var context = scope.ServiceProvider.GetRequiredService<MtgContext>();
context.Database.Migrate();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors(policy => policy.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
//app.UseCors(policy => policy.WithOrigins("http://localhost:3001/")

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
