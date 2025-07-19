using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InsureFlowAI.DAL.Abstract;
using InsureFlowAI.DAL.Models.DataModel;

namespace InsureFlowAI.DAL.Repositories
{
    public class GenericRepository<T> : IGenericDal<T> where T : class
    {
        private readonly InsureFlowAIDBEntities _context;

        public GenericRepository(InsureFlowAIDBEntities context)
        {
            _context = context;
        }

        public void Delete(int id)
        {
            var value = _context.Set<T>().Find(id);
            _context.Set<T>().Remove(value);
            _context.SaveChanges();
        }

        public List<T> GetAll()
        {
            var values = _context.Set<T>().ToList();
            return values;
        }

        public T GetById(int id)
        {
            var value = _context.Set<T>().Find(id);
            return value;
        }

        public void Insert(T entity)
        {
            _context.Set<T>().Add(entity);
            _context.SaveChanges();
        }

        public void Update(T entity)
        {
            throw new NotImplementedException();
        }
    }
}
