using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebShop.Models;

namespace RefactorToBeTestable.DataAccessLayer
{
    public interface IShopContext : IDisposable
    {
        DbSet<User> Users { get; set; }

        int SaveChanges();
        void MarkAsModified(object modified);

    }
}
