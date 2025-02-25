using ASPNetExapp.Data;
using ASPNetExapp.Models;

namespace ASPNetExapp.Services;

public class UserService
{
    private readonly AppDbContext _context;

    public UserService(AppDbContext context)
    {
        _context = context;
    }

    public PaginatedResult<User> GetUsers(string? query, int page, int pageSize)
    {
        var filteredUsers = _context.Users.AsQueryable();

        if (!string.IsNullOrEmpty(query))
        {
            filteredUsers = filteredUsers.Where(u =>
                u.Name.Contains(query, StringComparison.OrdinalIgnoreCase) || u.Email.Contains(query, StringComparison.OrdinalIgnoreCase));
        }

        var totalRecords = filteredUsers.Count();

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

    public User? GetUserById(int id) => _context.Users.FirstOrDefault(u => u.Id == id);

    public User CreateUser(User newUser)
    {
        _context.Users.Add(newUser);
        _context.SaveChanges();
        return newUser;
    }

    public bool UpdateUser(int id, User updatedUser)
    {
        var user = _context.Users.Find(id);
        if (user == null) return false;

        user.Name = updatedUser.Name;
        user.Email = updatedUser.Email;
        _context.SaveChanges();
        return true;
    }

    public bool DeleteUser(int id)
    {
        var user = _context.Users.Find(id);
        if (user == null) return false;

        _context.Users.Remove(user);
        _context.SaveChanges();
        return true;
    }
}
