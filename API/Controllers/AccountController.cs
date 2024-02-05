using System.Security.Cryptography;
using System.Text;
using API.Data;
using API.DTOs;
using API.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers;

public class AccountController : BaseApiController
{
    private readonly DataContext _context;
    private readonly ITokenService _tokenService;

    public AccountController(DataContext context, ITokenService tokenService)
    {
        _context = context;
        _tokenService = tokenService;
    }

    [HttpPost("register")]  // POST: api/account/register
    public async Task<ActionResult<UserDto>> Register(RegisterDto registerDto)
    {
        if (await UserExists(registerDto.Username)) return new BadRequestObjectResult("Username is taken");
        using var hmac = new HMACSHA512();

        var user = new AppUser {
            UserName = registerDto.Username.ToLower(),
            PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(registerDto.Password)),
            PasswordSalt = hmac.Key
        };
        _context.Users.Add(user);
        await _context.SaveChangesAsync();
        return new UserDto
        {
            Username = user.UserName,
            Token = _tokenService.CreateToken(user)
        };
    }

    [HttpPost("login")]
    public async Task<ActionResult<UserDto>> Login(LoginDto loginDto)
    {
        var providedUsername = loginDto.Username;
        var providedPassword = loginDto.Password;
        var user = await _context.Users.SingleOrDefaultAsync(x => x.UserName == providedUsername.ToLower());
        if (user ==  null)
        {
            return new UnauthorizedObjectResult("Invalid Username");
        }
        byte[] passwordSalt = user.PasswordSalt;
        using var hmac = new HMACSHA512(passwordSalt);
        byte [] providedPasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(providedPassword));
        byte [] storedPasswordHash = user.PasswordHash;
        for (int i = 0; i < providedPasswordHash.Length; i++)
        {
            if (providedPasswordHash[i] != storedPasswordHash[i])
            {
                return new UnauthorizedObjectResult("Invalid Password");
            }
        }

         return new UserDto
        {
            Username = user.UserName,
            Token = _tokenService.CreateToken(user)
        };
   }

    public async Task<bool> UserExists(string username)
    {
        return await _context.Users.AnyAsync(x => x.UserName == username.ToLower());
    }

}

