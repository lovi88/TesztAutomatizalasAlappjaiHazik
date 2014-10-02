using RefactorToBeTestable.DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace WebShopApiTest.MyFakes
{
    class FakeShopContext : IShopContext
    {

        public FakeShopContext()
        {
            this.Users = new TestUsersDbSet();
        }

        public DbSet<WebShop.Models.User> Users { get; set; }


        public int SaveChanges()
        {
            return 0;
        }

        public void MarkAsModified(object modified) { }

        public void Dispose() { }

    }
}
