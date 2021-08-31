using Microsoft.EntityFrameworkCore;
using MultipleTableCrudPractice.Data.DataContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultipleTableCrudPractice.Repository.AsyncRepository
{
    public interface IRepositoryAsync<TEntity> where TEntity : class
    {
       
        IEnumerable<TEntity> GetOnlyList();
        //Task<TEntity> GetById(int Id);
        Task<TEntity> InsertData(TEntity entity);
        Task UpdateData(TEntity entity);
        Task DeleteData(TEntity entity);
    }

    public class RepositoryAsync<TEntity> : IRepositoryAsync<TEntity> where TEntity : class
    {
        private readonly CrudContext _context;

        public RepositoryAsync(CrudContext context)
        {
            _context = context;
        }
        public async Task DeleteData(TEntity entity)
        {
            _context.Set<TEntity>().Remove(entity);
            await _context.SaveChangesAsync();
        }

        //public async Task<TEntity> GetById(int Id)
        //{
        //    return await _context.Set<TEntity>().AsNoTracking().FirstOrDefaultAsync(e => e.Id == id);
        //}

        public IEnumerable<TEntity> GetOnlyList()
        {
            return _context.Set<TEntity>();
        }

        public async Task<TEntity> InsertData(TEntity entity)
        {
            var obj = await _context.Set<TEntity>().AddAsync(entity);
            await _context.SaveChangesAsync();
            return obj.Entity;
        }

        public async Task UpdateData(TEntity entity)
        {
            _context.Set<TEntity>().Update(entity).State = EntityState.Modified;
            //_context.Entry<TEntity>(entity).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }
    }
}
