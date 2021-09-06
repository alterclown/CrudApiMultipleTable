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
        Task<EmployeeDto> InsertDataDTO(EmployeeDto dto);
        Task<ManyEmployeeDto> InsertDataMany(ManyEmployeeDto dto);
        Task<EmployeeDetails> InsertDataObjectWithListVm(EmployeeDetails employeeAddressVM);
        Task<string> UpdateData(int id, EmployeeDetails employeeDetails);
        Task<EmployeeAddressVM> UpdateDataVm(int id,EmployeeAddressVM employeeAddressVM);
        Task<string> DeleteData(int id);
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
        #region Insert Two Objects
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

        #endregion

        #region Insert Object With List
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
        #endregion
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
        #region Insert Data One to One
        public async Task<EmployeeDto> InsertDataDTO(EmployeeDto dto)
        {
            try
            {
                var emp = new EmployeeDetails()
                {
                    EmployeeId = dto.EmployeeId,
                    EmployeeName = dto.EmployeeName,
                    Designation = dto.Designation
                };
                await _context.EmployeeDetailes.AddAsync(emp);
                await _context.SaveChangesAsync();
                var dataId = await _context.EmployeeDetailes.FindAsync(emp.EmployeeId);
                var add = new AddressDetails()
                {
                EmployeeId = dataId.EmployeeId,
                EmployeeAddress = dto.Add.EmployeeAddress,
                AddressType = dto.Add.AddressType
                };
                _context.AddressDetailes.Add(add);
                await _context.SaveChangesAsync();
                return dto;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region Dto List + Object
        public async Task<ManyEmployeeDto> InsertDataMany(ManyEmployeeDto dto)
        {
            try
            {
                var emp = new EmployeeDetails()
                {
                    EmployeeId = dto.EmployeeDetails.EmployeeId,
                    EmployeeName = dto.EmployeeDetails.EmployeeName,
                    Designation = dto.EmployeeDetails.Designation
                };
                await _context.EmployeeDetailes.AddAsync(emp);
                await _context.SaveChangesAsync();
                var dataId = await _context.EmployeeDetailes.FindAsync(emp.EmployeeId);

                foreach (var address in dto.Address)
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
                return dto;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region Update two objects
        public async Task<EmployeeAddressVM> UpdateDataVm(int id,EmployeeAddressVM employeeAddressVM)
        {
            try
            {
                var intermediary = _context.EmployeeDetailes.Where(x => x.EmployeeId == employeeAddressVM.EmployeeId).FirstOrDefault();
                if (intermediary != null)
                {
                    
                    intermediary.EmployeeName = employeeAddressVM.EmployeeName;
                    intermediary.Designation = employeeAddressVM.Designation;
                    _context.EmployeeDetailes.Update(intermediary);
                    foreach (var res in intermediary.Address)
                    {
                        res.EmployeeAddress = employeeAddressVM.EmployeeAddress;
                        res.AddressType = employeeAddressVM.AddressType;
                        _context.AddressDetailes.Update(res);
                    }
                    //rest of updates
                    await _context.SaveChangesAsync();
                    
                }
                return employeeAddressVM;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

    }
}
