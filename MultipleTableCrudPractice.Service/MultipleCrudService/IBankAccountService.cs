using MultipleTableCrudPractice.Data.Entities;
using MultipleTableCrudPractice.Repository.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultipleTableCrudPractice.Service.MultipleCrudService
{
    public interface IBankAccountService
    {
        Task<IEnumerable<BankAccount>> GetOnlyList();
        Task<BankAccount> GetById(int Id);
        Task<BankAccount> InsertData(BankAccount bankAccount);
        Task<string> UpdateData(int id, BankAccount bankAccount);
        Task<string> DeleteData(int id);
    }

    public class BankAccountService : IBankAccountService
    {
        private readonly IBankAccountRepository _repository;

        public BankAccountService(IBankAccountRepository repository)
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

        public async Task<BankAccount> GetById(int Id)
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

        public async Task<IEnumerable<BankAccount>> GetOnlyList()
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

        public async Task<BankAccount> InsertData(BankAccount bankAccount)
        {
            try
            {
                var res = await _repository.InsertData(bankAccount);
                return res;
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
                var res = await _repository.UpdateData(id, bankAccount);
                return res;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}
