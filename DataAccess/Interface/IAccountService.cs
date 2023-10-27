using Domain.Entities;

namespace Infrastructure.Interface
{
    public interface IAccountService
    {
        public User Login(string username, string password);
    }
}
