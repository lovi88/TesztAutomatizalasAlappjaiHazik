using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebShop.Models;

namespace WebShopApiTest.MyFakes
{
    class TestUsersDbSet: TestDbSet<User>
    {
        public override User Find(params object[] keyValues)
        {
            return this.SingleOrDefault(user => user.UId == (int)keyValues.Single());
        }

    }
}
