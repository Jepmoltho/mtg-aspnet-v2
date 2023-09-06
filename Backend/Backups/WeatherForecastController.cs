/*
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Backend.Controllers;

[ApiController]
[Route("[controller]")]
public class WeatherForecastController : ControllerBase
{
    private static readonly string[] Summaries = new[]
    {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

    private readonly ILogger<WeatherForecastController> _logger;
    private readonly MtgContext _context;

    public WeatherForecastController(ILogger<WeatherForecastController> logger, MtgContext context)
    {
        _logger = logger;
        _context = context;
    }

    [HttpGet("users")]    
    public ActionResult<IEnumerable<User>> GetUsers()
    {
        var users = _context.Users
        .Include(u => u.Cards)
        .ToList();
        
        return Ok(users);
    }

   [HttpGet("user/{userId}")]    
    public ActionResult<IEnumerable<User>> GetUser(int userId)
    {
        var users = _context.Users
        .Include(u => u.Cards)
        .FirstOrDefault(u => u.UserId == userId);
        
        return Ok(users);
    }


    [HttpPost("user")]
    public ActionResult<int> PostUser(User user)
    {
        _context.Entry(user).State = EntityState.Added;
        //_context.Users.Add(user);
        _context.SaveChanges();

        return Ok(user.UserId);
    }


    [HttpGet("get2")]
    public IEnumerable<WeatherForecast> Get()
    {
        return Enumerable.Range(1, 5).Select(index => new WeatherForecast
        {
            Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
            TemperatureC = Random.Shared.Next(-20, 55),
            Summary = Summaries[Random.Shared.Next(Summaries.Length)]
        })
        .ToArray();
    }

    [HttpGet("get1")]
    public IEnumerable<WeatherForecast> Get2()
    {
        return Enumerable.Range(1, 5).Select(index => new WeatherForecast
        {
            Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
            TemperatureC = Random.Shared.Next(-20, 55),
            Summary = Summaries[Random.Shared.Next(Summaries.Length)]
        })
        .ToArray();
    }
}
*/