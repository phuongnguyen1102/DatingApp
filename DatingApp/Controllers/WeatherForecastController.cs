using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DatingApp.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace DatingApp.Controllers
{
  [ApiController]
  [Route("api/[controller]")]
  public class WeatherForecastController : ControllerBase
  {
    // private readonly ILogger<WeatherForecastController> _logger;
    private readonly DataContext _context;

    // public WeatherForecastController (ILogger<WeatherForecastController> logger) {
    //   _logger = logger;
    // }
    public WeatherForecastController(DataContext context)
    {
      _context = context;

    }

    [HttpGet]
    // public IEnumerable<WeatherForecast> Get()
    // {
    //     var rng = new Random();
    //     return Enumerable.Range(1, 5).Select(index => new WeatherForecast
    //     {
    //         Date = DateTime.Now.AddDays(index),
    //         TemperatureC = rng.Next(-20, 55),
    //         Summary = Summaries[rng.Next(Summaries.Length)]
    //     })
    //     .ToArray();
    // }
    public async Task<IActionResult> GetValues()
    {
      var values = await _context.Values.ToListAsync();
      return Ok(values);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetValue(int id) {
      var value = await _context.Values.FirstOrDefaultAsync(x => x.Id == id);
      return Ok(value);
    }
  }
}