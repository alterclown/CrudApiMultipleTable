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
    public interface IAddressRepository
    {
        Task<IEnumerable<AddressDetails>> GetOnlyList();
        Task<AddressDetails> GetById(int Id);
        Task<AddressDetails> InsertData(AddressDetails address);
        Task<string> UpdateData(int id, AddressDetails address);
        Task<string> DeleteData(int id);
    }

    public class AddressRepository : IAddressRepository
    {
        private readonly CrudContext _context;
        public AddressRepository(CrudContext context)
        {
            _context = context;
        }
        public async Task<string> DeleteData(int id)
        {
            try
            {
                var res = await _context.AddressDetailes.FindAsync(id);
                _context.AddressDetailes.Remove(res);
                await _context.SaveChangesAsync();
                return "Deleted";
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<AddressDetails> GetById(int Id)
        {
            try
            {
                var res = await _context.AddressDetailes.FirstOrDefaultAsync(m => m.AddressId == Id);
                return res;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<IEnumerable<AddressDetails>> GetOnlyList()
        {
            try
            {
                var response = from c in _context.AddressDetailes
                               orderby c.AddressId descending
                               select c;
                return await response.ToListAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<AddressDetails> InsertData(AddressDetails address)
        {
            try
            {
                _context.AddressDetailes.Add(address);
                await _context.SaveChangesAsync();
                return address;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<string> UpdateData(int id, AddressDetails address)
        {
            try
            {
                var res = await _context.AddressDetailes.FirstOrDefaultAsync(m => m.AddressId == id);
                res.EmployeeAddress = address.EmployeeAddress;
                res.AddressType = address.AddressType;
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
