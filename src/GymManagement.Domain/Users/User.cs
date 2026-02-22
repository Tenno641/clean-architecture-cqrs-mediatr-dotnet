using GymManagement.Domain.Common.Interfaces;

namespace GymManagement.Domain.Users;

public class User 
{
    public Guid Id { get; private set; }
    public string FirstName { get; private set; }
    public string LastName { get; private set; }
    public string Email { get; private set; }
    private string _passwordHash;
    public User(string firstName, string lastName, string email, string passwordHash, Guid? id = null)
    {
        FirstName = firstName;
        LastName = lastName;
        Email = email;
        _passwordHash = passwordHash;
        Id = id ?? Guid.CreateVersion7();
    }

    public bool IsCorrectPassword(string password, IPasswordHasher passwordHasher)
    {
        return passwordHasher.IsCorrectPassword(password, _passwordHash);
    }
    
    private User() { }
}