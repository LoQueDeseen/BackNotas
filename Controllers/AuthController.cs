using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using BackNotas.Models;
using BackNotas.Data;
using System.Linq;
using System.Security.Cryptography;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly NotasContext _context;
    private readonly string _secretKey;

    public AuthController(NotasContext context)
    {
        _context = context;
        _secretKey = GenerateSecretKey(32); // Genera una clave de 32 bytes (256 bits)
    }

    [HttpPost("login")]
    public IActionResult Login([FromBody] LoginRequest loginRequest)
    {
        Console.WriteLine("user" + loginRequest.UserName + " password: " + loginRequest.Password);

        var user = _context.Users.FirstOrDefault(u => u.UserName == loginRequest.UserName && u.Password == loginRequest.Password);

        if (user == null)
            return Unauthorized();

        var token = GenerateJwtToken(user.UserName); // Generar token JWT

        return Ok(new { token });
    }

    private string GenerateJwtToken(string username)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(_secretKey);
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

    public class LoginRequest
    {
        public string UserName { get; set; }
        public string Password { get; set; }
    }

    private string GenerateSecretKey(int keyLengthInBytes)
    {
        int keyLengthInBits = keyLengthInBytes * 8;
        byte[] keyBytes = new byte[keyLengthInBytes];
        using (var rng = new RNGCryptoServiceProvider())
        {
            rng.GetBytes(keyBytes);
        }
        string secretKey = BitConverter.ToString(keyBytes).Replace("-", "");
        return secretKey;
    }
}






// using Microsoft.AspNetCore.Mvc;
// using Microsoft.IdentityModel.Tokens;
// using System;
// using System.IdentityModel.Tokens.Jwt;
// using System.Security.Claims;
// using System.Text;
// using BackNotas.Models;
// using BackNotas.Data;
// using Microsoft.AspNetCore.Identity;


// [ApiController]
// [Route("api/[controller]")]
// public class AuthController : ControllerBase
// {
//     private readonly NotasContext _context;

//     public AuthController(NotasContext context)
//     {
//         _context = context;
//     }

//     [HttpPost("login")]
// public IActionResult Login([FromBody] LoginRequest loginRequest)
// {
//     Console.WriteLine("user" + loginRequest.UserName + " password: " + loginRequest.Password);

//     var user = _context.Users.FirstOrDefault(u => u.UserName == loginRequest.UserName && u.Password == loginRequest.Password);
    
//     if (user == null)
//         return Unauthorized();

//     var token = GenerateJwtToken(user.UserName); // Generar token JWT

//     return Ok(new { token });
// }
// public class LoginRequest
// {
//     public string UserName { get; set; }
//     public string Password { get; set; }
// }



//     private string GenerateJwtToken(string username)
//     {
//         var tokenHandler = new JwtSecurityTokenHandler();
//         var key = Encoding.ASCII.GetBytes("tu_clave_secreta");
//         var tokenDescriptor = new SecurityTokenDescriptor
//         {
//             Subject = new ClaimsIdentity(new Claim[]
//             {
//                 new Claim(ClaimTypes.Name, username)
//             }),
//             Expires = DateTime.UtcNow.AddHours(1), // Cambia el tiempo de expiración si lo necesitas
//             SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
//         };
//         var token = tokenHandler.CreateToken(tokenDescriptor);
//         return tokenHandler.WriteToken(token);
//     }
// }