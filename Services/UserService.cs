using ASPNetExapp.Models;

namespace ASPNetExapp.Services;

public class UserService
{
    private readonly List<User> _users = new()
    {
        new User { Id = 1, Name = "John Doe", Email = "john@example.com" },
        new User { Id = 2, Name = "Jane Smith", Email = "jane@example.com" },
        new User { Id = 3, Name = "Alice Johnson", Email = "alice@example.com" },
        new User { Id = 4, Name = "Bob Brown", Email = "bob@example.com" }
    };

    public PaginatedResult<User> GetUsers(string? query, int page, int pageSize)
    {
        var filteredUsers = _users
            .Where(u => string.IsNullOrEmpty(query) ||
                        u.Name.Contains(query, StringComparison.OrdinalIgnoreCase) ||
                        u.Email.Contains(query, StringComparison.OrdinalIgnoreCase))
            .ToList();

        var totalRecords = filteredUsers.Count;

        var usersPage = filteredUsers
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToList();

        return new PaginatedResult<User>
        {
            Data = usersPage,
            TotalRecords = totalRecords
        };
    }

    public User? GetUserById(int id) => _users.FirstOrDefault(u => u.Id == id);

    public User CreateUser(User newUser)
    {
        newUser.Id = _users.Count > 0 ? _users.Max(u => u.Id) + 1 : 1;
        _users.Add(newUser);
        return newUser;
    }

    public bool UpdateUser(int id, User updatedUser)
    {
        var user = GetUserById(id);
        if (user == null) return false;

        user.Name = updatedUser.Name;
        user.Email = updatedUser.Email;
        return true;
    }

    public bool DeleteUser(int id)
    {
        var user = GetUserById(id);
        if (user == null) return false;

        _users.Remove(user);
        return true;
    }
}
