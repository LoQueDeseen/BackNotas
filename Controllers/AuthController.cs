using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using BackNotas.Models;
using BackNotas.Data;


[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly NotasContext _context;

    public AuthController(NotasContext context)
    {
        _context = context;
    }

    // [HttpPost("login")]
    [HttpGet("{username}/{pass}")]
    public IActionResult Login(string username, string pass) 
    {
        // Console.WriteLine("user" + model.UserName + " password: " + model.Password);
        Console.WriteLine("user" + username + " password: " + pass);

        // var user = _context.Users.FirstOrDefault(u => u.UserName == model.UserName && u.Password == model.Password);
         var user = _context.Users.FirstOrDefault(u => u.UserName == username && u.Password == pass);

        if (user == null)
            return Unauthorized();

        // var token = GenerateJwtToken(username); // Generar token JWT

        return Ok(new { user });
    }

    private string GenerateJwtToken(string username)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes("tu_clave_secreta"); // Cambia por tu clave secreta
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new Claim[]
            {
                new Claim(ClaimTypes.Name, username)
            }),
            Expires = DateTime.UtcNow.AddHours(1), // Cambia el tiempo de expiración si lo necesitas
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };
        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }
}

