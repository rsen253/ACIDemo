using ACIDemo.Dtos;
using ACIDemo.Models;
using Bogus;
using Mapster;
using Microsoft.EntityFrameworkCore;

namespace ACIDemo.Data;

public interface IUserRepository
{
    Task<List<UserResult>> GetAllUsers();

    Task<bool> CreateNewUser(UserDto user);

    Task<string> CreateListOfUsers();

    Task<UserResult> GetUserDetailsById(Guid id);
}

public sealed class UserRepository(UserDbContext context) : IUserRepository
{
    public async Task<string> CreateListOfUsers()
    {
        var faker = GetUserGenerator();
        var generatedData = faker.Generate(5);
        var usersList = generatedData.Adapt<List<User>>();
        await context.Users.AddRangeAsync(usersList);
        await context.SaveChangesAsync();
        return $"{generatedData.Count} user has been inserted successfully";
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

    private static Faker<UserDto> GetUserGenerator()
    {
        return new Faker<UserDto>()
            .RuleFor(e => e.Id, _ => Guid.NewGuid())
            .RuleFor(e => e.FirstName, f => f.Name.FirstName())
            .RuleFor(e => e.Email, (f, e) => f.Internet.Email(e.FirstName))
            .RuleFor(e => e.Country, e => e.Address.Country());
    }
}
