using API.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[Authorize]
public class UsersController : BaseApiController
{
    private IUserRepository _userRepository;

    public UsersController(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }
    [HttpGet]   // api/users
    public async Task<ActionResult<IEnumerable<AppUser>>> GetUsers()
    {
        return new OkObjectResult(await _userRepository.GetUsersAsync());
    }
    [HttpGet("{userName}")]   // api/users/3
    public async Task<ActionResult<AppUser>> GetUser(string username)
    {
        return await _userRepository.GetUserByUsernameAsync(username);
    }

}
