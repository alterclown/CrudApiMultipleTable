using Microsoft.EntityFrameworkCore;
using MultipleTableCrudPractice.Data.DataContext;
using MultipleTableCrudPractice.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultipleTableCrudPractice.Repository.Repositories
{
    public interface ISalaryRepository
    {
        Task<IEnumerable<Salary>> GetOnlyList();
        Task<Salary> GetById(int Id);
        Task<Salary> InsertData(Salary salary);
        Task<string> UpdateData(int id,Salary salary);
        Task<string> DeleteData(int id);
    }

    public class SalaryRepository : ISalaryRepository
    {
        private readonly CrudContext _context;
        public SalaryRepository(CrudContext context)
        {
            _context = context;
        }
        public async Task<string> DeleteData(int id)
        {
            try
            {
                var res = await _context.Salaries.FindAsync(id);
                _context.Salaries.Remove(res);
                await _context.SaveChangesAsync();
                return "Deleted";
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<Salary> GetById(int Id)
        {
            try
            {
                var res = await _context.Salaries.FirstOrDefaultAsync(m => m.SalaryId == Id);
                return res;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task <IEnumerable<Salary>> GetOnlyList()
        {
            try
            {
                var response = from c in _context.Salaries
                               orderby c.SalaryId descending
                               select c;
                return await response.ToListAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<Salary> InsertData(Salary salary)
        {
            try
            {
                _context.Salaries.Add(salary);
                await _context.SaveChangesAsync();
                return salary;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<string> UpdateData(int id,Salary salary)
        {
            try
            {
                var res = await _context.Salaries.FirstOrDefaultAsync(m => m.SalaryId == id);
                res.SalaryAmount = salary.SalaryAmount;
                _context.Update(res);
                await _context.SaveChangesAsync();
                return "Updated Record";
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
