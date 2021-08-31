using Microsoft.EntityFrameworkCore;
using MultipleTableCrudPractice.Data.DataContext;
using MultipleTableCrudPractice.Data.Entities;
using MultipleTableCrudPractice.Repository.AsyncRepository;
using MultipleTableCrudPractice.Repository.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultipleTableCrudPractice.Service.MultipleCrudService
{
    public interface ISalaryService
    {
        Task<IEnumerable<Salary>> GetOnlyList();
        Task<Salary> GetById(int Id);
        Task<Salary> InsertData(Salary salary);
        Task<string> UpdateData(int id, Salary salary);
        Task<string> DeleteData(int id);
    }

    public class SalaryService : ISalaryService
    {
        private readonly ISalaryRepository _repository;
        public SalaryService(ISalaryRepository repository)
        {
            _repository = repository;
        }
        public async Task<string> DeleteData(int id)
        {
            try
            {
                var delete = await _repository.DeleteData(id);
                return delete;
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
                var get = await _repository.GetById(Id);
                return get;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public async Task<IEnumerable<Salary>> GetOnlyList()
        {
            try
            {
                var getList = await _repository.GetOnlyList();
                return getList;
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
                var insert = await _repository.InsertData(salary);
                return insert;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public async Task<string> UpdateData(int id, Salary salary)
        {
            try
            {
                var up = await _repository.UpdateData(id, salary);
                return up;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}
