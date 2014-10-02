using System;
using WebShop.Models;
namespace RefactorToBeTestable.DataAccessLayer
{
    public interface IUserRepository : IRepository<User>, IDisposable
    {
        User GetUserByNameAndPassword(string Name, string PasswordHash);
    }
}
