using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DomainClasses;

namespace DataLayer.DAL
{
    public interface IRepository<T>
    {
        IEnumerable<T> Get();
        T GetById(int id);
        void InsertObject(T obj);
        void DeleteObject(int id);
        void UpdateObject(T obj);
        void Dispose(bool disposing);
        void Save();
    }
}
