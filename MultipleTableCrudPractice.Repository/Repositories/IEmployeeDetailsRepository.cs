using Microsoft.EntityFrameworkCore;
using MultipleTableCrudPractice.Data.DataContext;
using MultipleTableCrudPractice.Data.DTOs;
using MultipleTableCrudPractice.Data.Entities;
using MultipleTableCrudPractice.Data.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultipleTableCrudPractice.Repository.Repositories
{
    public interface IEmployeeDetailsRepository
    {
        Task<IEnumerable<EmployeeDetails>> GetOnlyList();
        Task<EmployeeDetails> GetById(int Id);
        Task<EmployeeDetails> InsertData(EmployeeDetails employeeDetails);
        Task<EmployeeAddressVM> InsertDataVm(EmployeeAddressVM employeeAddressVM);
        Task<EmployeeDetails> InsertDataObjectWithListVm(EmployeeDetails employeeAddressVM);
        Task<(EmployeeDetails, List<AddressDetails>)> PostEmployee(EmployeeDetails emp, List<AddressDetails> addressList);
        Task<string> UpdateData(int id, EmployeeDetails employeeDetails);
        Task<string> DeleteData(int id);
        Task<string> UpdateEmployeeComplex(EmployeeDto updateModel);
    }

    public class EmployeeDetailsRepository : IEmployeeDetailsRepository
    {
        private readonly CrudContext _context;
        public EmployeeDetailsRepository(CrudContext context)
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

        public async Task<EmployeeDetails> GetById(int Id)
        {
            try
            {
                var res = await _context.EmployeeDetailes.FirstOrDefaultAsync(m => m.EmployeeId == Id);
                return res;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<IEnumerable<EmployeeDetails>> GetOnlyList()
        {
            try
            {
                var response = from c in _context.EmployeeDetailes
                               orderby c.EmployeeId descending
                               select c;
                return await response.ToListAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<EmployeeDetails> InsertData(EmployeeDetails employeeDetails)
        {
            try
            {
                _context.EmployeeDetailes.Add(employeeDetails);
                await _context.SaveChangesAsync();
                return employeeDetails;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<EmployeeAddressVM> InsertDataVm(EmployeeAddressVM employeeAddressVM)
        {
            try
            {
                var emp = new EmployeeDetails()
                {
                    EmployeeId = employeeAddressVM.EmployeeId,
                    EmployeeName = employeeAddressVM.EmployeeName,
                    Designation = employeeAddressVM.Designation
                };

                _context.EmployeeDetailes.Add(emp);
                await _context.SaveChangesAsync();

                var address = new AddressDetails()
                {
                    AddressId = employeeAddressVM.EmployeeId,
                    EmployeeId = emp.EmployeeId,
                    EmployeeAddress = employeeAddressVM.EmployeeAddress,
                    AddressType = employeeAddressVM.AddressType
                };
                _context.AddressDetailes.Add(address);
                await _context.SaveChangesAsync();
                return employeeAddressVM;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<EmployeeDetails> InsertDataObjectWithListVm(EmployeeDetails employeeAddressVM)
        {
            try
            {
                var emp = new EmployeeDetails()
                {
                    EmployeeName = employeeAddressVM.EmployeeName,
                    Designation = employeeAddressVM.Designation
                };
                _context.EmployeeDetailes.Add(emp);
                await _context.SaveChangesAsync();

                var dataId = await _context.EmployeeDetailes.FindAsync(emp.EmployeeId);

                foreach (var address in employeeAddressVM.Address)
                {
                    var add = new AddressDetails()
                    {
                        EmployeeId = dataId.EmployeeId,
                        EmployeeAddress = address.EmployeeAddress,
                        AddressType = address.AddressType
                    };
                    _context.AddressDetailes.Add(add);
                }
                await _context.SaveChangesAsync();
                return employeeAddressVM;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<string> UpdateData(int id, EmployeeDetails employeeDetails)
        {
            try
            {
                var res = await _context.EmployeeDetailes.FirstOrDefaultAsync(m => m.EmployeeId == id);
                res.EmployeeName = employeeDetails.EmployeeName;
                res.Designation = employeeDetails.Designation;
                _context.Update(res);
                await _context.SaveChangesAsync();
                return "Updated Record";
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<string> UpdateEmployeeComplex(EmployeeDto updateModel)
        {
            var blog = await _context.EmployeeDetailes
                .Include(x => x.Address)
                .FirstOrDefaultAsync(x => x.EmployeeId == updateModel.EmployeeId);

             _context.RemoveRange(blog.Address);

            await _context.AddRangeAsync(updateModel.Add.Select(x => new AddressDetails
            {
                EmployeeId = blog.EmployeeId,
                EmployeeAddress = x.EmployeeAddress,
                AddressType = x.AddressType
            }));

            await _context.SaveChangesAsync();
            blog.EmployeeName = updateModel.EmployeeName;
            blog.Designation = updateModel.Designation;
            _context.Update(blog);
            _context.SaveChanges();
            return "Updated";
        }

        public async Task<(EmployeeDetails,List<AddressDetails>)> PostEmployee(EmployeeDetails emp, List<AddressDetails> addressList)
        {
            try
            {
                var order = emp;
                order.Address = addressList;
                _context.EmployeeDetailes.Add(order);
                await _context.SaveChangesAsync();
                return (emp, addressList);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}
