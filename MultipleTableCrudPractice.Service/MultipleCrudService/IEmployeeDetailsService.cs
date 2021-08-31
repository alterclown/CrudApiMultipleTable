﻿using MultipleTableCrudPractice.Data.DTOs;
using MultipleTableCrudPractice.Data.Entities;
using MultipleTableCrudPractice.Data.ViewModels;
using MultipleTableCrudPractice.Repository.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultipleTableCrudPractice.Service.MultipleCrudService
{
    public interface IEmployeeDetailsService
    {
        Task<IEnumerable<EmployeeDetails>> GetOnlyList();
        Task<EmployeeDetails> GetById(int Id);
        Task<EmployeeDetails> InsertData(EmployeeDetails employeeDetails);
        Task<string> UpdateData(int id, EmployeeDetails employeeDetails);
        Task<string> DeleteData(int id);
        Task<EmployeeAddressVM> InsertDataVm(EmployeeAddressVM employeeAddressVM);
        Task<EmployeeDetails> InsertDataObjectWithListVm(EmployeeDetails employeeAddressVM);
        Task<string> UpdateEmployeeComplex(EmployeeDto updateModel);
    }

    public class EmployeeDetailsService : IEmployeeDetailsService
    {
        private readonly IEmployeeDetailsRepository _repository;

        public EmployeeDetailsService(IEmployeeDetailsRepository repository)
        {
            _repository = repository;
        }

        public async Task<string> DeleteData(int id)
        {
            try
            {
                var res = await _repository.DeleteData(id);
                return res;
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
                var res = await _repository.GetById(Id);
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
                var res = await _repository.GetOnlyList();
                return res;
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
                var res = await _repository.InsertData(employeeDetails);
                return res;
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
                var res = await _repository.InsertDataObjectWithListVm(employeeAddressVM);
                return res;
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
                var res = await _repository.InsertDataVm(employeeAddressVM);
                return res;
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
                var res = await _repository.UpdateData(id, employeeDetails);
                return res;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public async Task<string> UpdateEmployeeComplex(EmployeeDto updateModel)
        {
            try
            {
                var res = await _repository.UpdateEmployeeComplex(updateModel);
                return res;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}
