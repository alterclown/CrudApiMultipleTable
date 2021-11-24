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
        Task<EmployeeAddressVM> UpdateDataVm(EmployeeAddressVM employeeAddressVM);
        Task<EmployeeAddVM> UpdateDataSingleVm(EmployeeAddVM vM);
        Task<string> DeleteData(int id);
        Task<List<EmployeeAddressVM>> GetEmployeeAddressList();
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
        public async Task<EmployeeAddressVM> UpdateDataVm(EmployeeAddressVM employeeAddressVM)
        {
            try
            {
                var existingParent = _context.EmployeeDetailes
                                                .Where(p => p.EmployeeId == employeeAddressVM.EmployeeId)
                                                .Include(p => p.Address)
                                                .SingleOrDefault();

                if (existingParent != null)
                {
                    // Update parent
                    _context.Entry(existingParent).CurrentValues.SetValues(employeeAddressVM);

                    // Delete children
                    foreach (var existingChild in existingParent.Address.ToList())
                    {
                        if (!employeeAddressVM.Address.Any(c => c.AddressId == existingChild.AddressId))
                            _context.AddressDetailes.Remove(existingChild);
                    }

                    // Update and Insert children
                    foreach (var childModel in employeeAddressVM.Address)
                    {
                        var existingChild = existingParent.Address
                            .Where(c => c.AddressId == childModel.AddressId && c.AddressId != default(int))
                            .SingleOrDefault();

                        if (existingChild != null)
                            // Update child
                            _context.Entry(existingChild).CurrentValues.SetValues(childModel);
                        else
                        {
                            // Insert child
                            var newChild = new AddressDetails
                            {
                            AddressId = childModel.EmployeeId,
                            EmployeeId = childModel.EmployeeId,
                            EmployeeAddress = childModel.EmployeeAddress,
                            AddressType = childModel.AddressType,
                        };
                            existingParent.Address.Add(newChild);
                        }
                    }

                    await _context.SaveChangesAsync();
                }
                return employeeAddressVM;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<EmployeeAddVM> UpdateDataSingleVm(EmployeeAddVM vM)
        {
            try
            {
                var client = _context.EmployeeDetailes.SingleOrDefault(x => x.EmployeeId == vM.address.AddressId);
                //vM.address = client.Address;
                //_context.EmployeeDetailes.Update(vM);
                _context.SaveChanges();
                return vM;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<List<EmployeeAddressVM>> GetEmployeeAddressList()
        {
            try
            {
                var response = await  (from t1 in _context.EmployeeDetailes
                               join t2 in _context.AddressDetailes on t1.EmployeeId equals t2.EmployeeId
                                       select new EmployeeAddressVM 
                                       {   EmployeeId = t1.EmployeeId,
                                           EmployeeName=t1.EmployeeName,
                                           AddressId = t2.AddressId }).ToListAsync();


                return response;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

    }
}
