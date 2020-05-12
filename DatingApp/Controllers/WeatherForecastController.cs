using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DatingApp.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace DatingApp.Controllers {
  [Authorize]
  [Route ("api/[controller]")]
  [ApiController]
  public class WeatherForecastController : ControllerBase {
    private readonly DataContext _context;
    public WeatherForecastController (DataContext context) {
      _context = context;

    }

    [AllowAnonymous]
    [HttpGet]
    public async Task<IActionResult> GetValues () {
      var values = await _context.Values.ToListAsync ();
      return Ok (values);
    }

    [HttpGet ("{id}")]
    public async Task<IActionResult> GetValue (int id) {
      var value = await _context.Values.FirstOrDefaultAsync (x => x.Id == id);
      return Ok (value);
    }
  }
}