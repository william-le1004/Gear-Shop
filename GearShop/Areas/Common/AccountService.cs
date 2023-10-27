using Domain.Entities;
using Infrastructure.Interface;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Application.Common.ExtensionMethods;

namespace GearShopWeb.Areas.Common
{
    public class AccountService : IAccountService
    {
        private readonly ApplicationDbContext _db;
        public AccountService(ApplicationDbContext db)
        {
            _db = db;
        }
        public User Login(string username, string password)
        {
            var user = _db.Users.Include(u => u.Role).AsNoTracking().SingleOrDefault(x =>
                       x.UserName.Equals(username) &&
                       x.Password.Equals(password.MD5Hash()));
            return user;
        }
    }
}
