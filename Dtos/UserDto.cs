namespace ACIDemo.Dtos;

public class UserDto
{
    public string FirstName { get; set; }
    public string Email { get; set; }
    public string Country { get; set; }
    public Guid Id { get; } = Guid.NewGuid();
}