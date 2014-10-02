using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebShop.Models;

namespace RefactorToBeTestable.DataAccessLayer
{
    public class UserRepository : RefactorToBeTestable.DataAccessLayer.IUserRepository
    {
        IShopContext dbctx;

        public UserRepository()
        {
            dbctx = new ShopContext();
        }

        public UserRepository(IShopContext context)
        {
            dbctx = context;
        }



        public User GetUserByNameAndPassword(string Name, string PasswordHash)
        {
            User user = dbctx.Users.
                Where(x => x.Name == Name && x.PasswordHash == PasswordHash).FirstOrDefault();
            return user;
        }

        public void Update(User user)
        {

            if (!UserExists(user))
            {
                throw new ArgumentException("The provided user not found");
            }

            dbctx.MarkAsModified(user);
            dbctx.SaveChanges();
        }

        public void Create(User user)
        {

            if (IsEmailReserved(user))
            {
                throw new ArgumentException("The given email address is already used by another customer");
            }

            dbctx.Users.Add(user);
            dbctx.SaveChanges();
        }

        public void Delete(int id)
        {
            User user = dbctx.Users.Find(id);

            if (user == null)
            {
                throw new ArgumentException("User not found");
            }

            dbctx.Users.Remove(user);
            dbctx.SaveChanges();
        }

        virtual protected bool IsEmailReserved(User user)
        {
            var u = dbctx.Users.Where(x => x.Email == user.Email).FirstOrDefault();

            return u != null;
        }

        virtual protected bool UserExists(User user)
        {
            return this.UserExists(user.UId);
        }

        virtual protected bool UserExists(int id)
        {
            return dbctx.Users.Count(e => e.UId == id) > 0;
        }

        public void Dispose()
        {
            dbctx.Dispose();
        }
    }
}
