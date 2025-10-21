using Microsoft.EntityFrameworkCore;
using MoneyFlow.Context;
using MoneyFlow.Entities;
using MoneyFlow.Models;
using MoneyFlow.Utilities;

namespace MoneyFlow.Managers;

public class UserManager
{
    private readonly AppDbContext _context;

    public UserManager(AppDbContext context)
    {
        _context = context;
    }

    public UserVM? Login(LoginVM login)
    {
        var user = _context.User
            .Where(item => item.Email == login.Email
                           && item.Password == Sha256Hasher.ComputeSha256Hash(login.Password))
            .Select(item => new UserVM
            {
                UserId = item.UserId,
                FullName = item.FullName,
                Email =  item.Email,
            } )
            .FirstOrDefault();

        return user;
    }

    public int Register(UserVM user)
    {
        if (user.Password != user.RepeatPassword)
            throw new InvalidOperationException("The passwords are note the same.");

        string emailNormalized = user.Email.Trim().ToLower();
        var foundEmail = _context.User
            .AsNoTracking()
            .Any(item => item.Email.ToLower() == emailNormalized);
        
        if (foundEmail)
            throw new InvalidOperationException("The email addressis already exists.");

        var entity = new User
        {
            FullName = user.FullName,
            Email = user.Email,
            Password = Sha256Hasher.ComputeSha256Hash(user.Password)
        };
        _context.Add(entity);
       var affectedRows =  _context.SaveChanges();
       return affectedRows;
    }
}