using System.Collections.Generic;

namespace DAL
{
    public interface IGenericRepository<T> where T : class
    {
        IList<T> GetAll();
        T GetById(int id);
        void Insert(T obj);
        void Delete(int id);
        void Delete(T obj);
        void Update(T obj);
        void Save();
    }
}
