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
    public interface IBankAccountRepository
    {
        Task<IEnumerable<BankAccount>> GetOnlyList();
        Task<BankAccount> GetById(int Id);
        Task<BankAccount> InsertData(BankAccount bankAccount);
        Task<string> UpdateData(int id, BankAccount bankAccount);
        Task<string> DeleteData(int id);
    }

    public class BankAccountRepository : IBankAccountRepository
    {
        private readonly CrudContext _context;
        public BankAccountRepository(CrudContext context)
        {
            _context = context;
        }
        public async Task<string> DeleteData(int id)
        {
            try
            {
                var res = await _context.BankAccounts.FindAsync(id);
                _context.BankAccounts.Remove(res);
                await _context.SaveChangesAsync();
                return "Deleted";
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<BankAccount> GetById(int Id)
        {
            try
            {
                var res = await _context.BankAccounts.FirstOrDefaultAsync(m => m.BankAccountId == Id);
                return res;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<IEnumerable<BankAccount>> GetOnlyList()
        {
            try
            {
                var response = from c in _context.BankAccounts
                               orderby c.BankAccountId descending
                               select c;
                return await response.ToListAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<BankAccount> InsertData(BankAccount bankAccount)
        {
            try
            {
                _context.BankAccounts.Add(bankAccount);
                await _context.SaveChangesAsync();
                return bankAccount;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<string> UpdateData(int id, BankAccount bankAccount)
        {
            try
            {
                var res = await _context.BankAccounts.FirstOrDefaultAsync(m => m.BankAccountId == id);
                res.BankAccountName = bankAccount.BankAccountName;
                res.TotalAmount = bankAccount.TotalAmount;
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
