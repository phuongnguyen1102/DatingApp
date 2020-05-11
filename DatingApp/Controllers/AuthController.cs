using System.Threading.Tasks;
using DatingApp.Data;
using DatingApp.Dtos;
using DatingApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace DatingApp.Controllers {
  [Route ("api/[controller]")]
  [ApiController]
  public class AuthController : ControllerBase {
    private readonly IAuthRepository _repo;
    private readonly IConfiguration _config;
    public AuthController (IAuthRepository repo, IConfiguration config) {
      _config = config;
      _repo = repo;
    }

    [HttpPost ("register")]
    public async Task<IActionResult> Register (UserForRegisterDto userForRegisterDto) {
      userForRegisterDto.Username = userForRegisterDto.Username.ToLower ();

      if (await _repo.UserExists (userForRegisterDto.Username))
        return BadRequest ("username existed");

      var userToCreate = new User {
        Username = userForRegisterDto.Username
      };

      var createdUser = await _repo.Register (userToCreate, userForRegisterDto.Password);

      return StatusCode (201);
    }

    [HttpPost ("login")]
    public async Task<IActionResult> Login (UserForRegisterDto userForLoginDto) {
      var userFromRepo = await _repo.Login (userForLoginDto.Username, userForLoginDto.Password);

      if (userFromRepo == null)
        return Unauthorized ();
      var claims = new [] {
        new Claim (ClaimTypes.NameIdentifier, UserFromRepo.Id.ToString ()),
        new Claim (ClaimTypes.Name, userFromRepo.Username)
      };

      var key new SymmetricSecurityKey(Encoding.UTF8
        .GetBytes(_config.GetSection("AppSettings:Tokens").Value));
    
      var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSh512Signature);

      var tokenDescriptor = new ClaimsIdentity(claims),
      Expires = DateTime.Now.AddDays(1),
    }
  }
}