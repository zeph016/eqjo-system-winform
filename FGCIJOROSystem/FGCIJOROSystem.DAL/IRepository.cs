using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FGCIJOROSystem.DAL
{
    interface IRepository<T>
    {
        void Add(T obj);
        void Update(T obj);
        void Delete(T obj);
        List<T> GetAll();
        T FindByID(Int64 id);
        List<T> SearchBy(string whereQuery);
    }
}
