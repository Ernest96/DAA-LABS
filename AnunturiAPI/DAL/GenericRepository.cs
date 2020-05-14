using Domain;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace DAL
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        protected ApplicationDbContext _context;
        protected DbSet<T> table;

        public GenericRepository(ApplicationDbContext context)
        {
            _context = context;
            table = context.Set<T>();
        }

        public IList<T> GetAll()
        {
            return table.ToList();
        }

        public T GetById(int id)
        {
            return table.Find(id);
        }

        public void Insert(T obj)
        {
            table.Add(obj);
        }

        public void Update(T obj)
        {
            table.Attach(obj);
            _context.Entry(obj).State = EntityState.Modified;
        }

        public void Delete(int id)
        {
            T existing = table.Find(id);
            table.Remove(existing);
        }

        public void Delete(T obj)
        {
            _context.Set<T>().Remove(obj);
        }

        public void Save()
        {
            _context.SaveChanges();
        }
    }
}
