using ACIDemo.Data;
using ACIDemo.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace ACIDemo.Controllers;

[ApiController]
[Route("api/user")]
public class UserController(IUserRepository _userRepo) : ControllerBase
{
    [HttpPost("CreateListOfuser")]
    public async Task<IActionResult> CreateListOfuser([FromBody] List<UserDto> users)
    {
        var result = await _userRepo.CreateListOfUsers(users);
        return new OkObjectResult(result);
    }

    [HttpPost]
    public async Task<IActionResult> CreateNewUser([FromBody] UserDto user)
    {
        var result = await _userRepo.CreateNewUser(user);
        return new OkObjectResult(result);
    }

    [HttpGet]
    public async Task<IActionResult> GetAllUsers()
    {
        var result = await _userRepo.GetAllUsers();
        return new OkObjectResult(result);
    }

    [HttpGet("id")]
    public async Task<IActionResult> GetUserById(Guid id)
    {
        var result = await _userRepo.GetUserDetailsById(id);
        return new OkObjectResult(result);
    }
}
