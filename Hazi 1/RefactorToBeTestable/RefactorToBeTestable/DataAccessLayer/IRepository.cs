using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RefactorToBeTestable.DataAccessLayer
{
    public interface IRepository<T>
    {
        void Create(T entity);
        void Delete(int id);

        void Update(T entity);

    }
}
