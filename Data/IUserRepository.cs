using ACIDemo.Dtos;
using ACIDemo.Models;
using Mapster;
using Microsoft.EntityFrameworkCore;

namespace ACIDemo.Data;

public interface IUserRepository
{
    Task<List<UserResult>> GetAllUsers();

    Task<bool> CreateNewUser(UserDto user);

    Task<string> CreateListOfUsers(List<UserDto> users);

    Task<UserResult> GetUserDetailsById(Guid id);
}

public sealed class UserRepository(UserDbContext context) : IUserRepository
{
    public async Task<string> CreateListOfUsers(List<UserDto> users)
    {
        var usersList = users.Adapt<List<User>>();
        await context.Users.AddRangeAsync(usersList);
        await context.SaveChangesAsync();
        return $"{users.Count} user has been inserted successfully";
    }

    public async Task<bool> CreateNewUser(UserDto user)
    {
        var usersList = user.Adapt<User>();
        await context.Users.AddAsync(usersList);
        await context.SaveChangesAsync();
        return true;
    }

    public async Task<List<UserResult>> GetAllUsers()
    {
        var result = await context.Users.ToListAsync();
        return result.Adapt<List<UserResult>>();
    }

    public async Task<UserResult> GetUserDetailsById(Guid id)
    {
        var result = await context.Users.FirstOrDefaultAsync(x => x.Id == id);
        return result.Adapt<UserResult>();
    }
}
