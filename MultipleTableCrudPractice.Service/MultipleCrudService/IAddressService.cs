using MultipleTableCrudPractice.Data.Entities;
using MultipleTableCrudPractice.Repository.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultipleTableCrudPractice.Service.MultipleCrudService
{
    public interface IAddressService
    {
        Task<IEnumerable<AddressDetails>> GetOnlyList();
        Task<AddressDetails> GetById(int Id);
        Task<AddressDetails> InsertData(AddressDetails addressDetails);
        Task<string> UpdateData(int id, AddressDetails addressDetails);
        Task<string> DeleteData(int id);
    }

    public class AddressService : IAddressService
    {
        private readonly IAddressRepository _repository;
        public AddressService(IAddressRepository repository)
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

        public async Task<AddressDetails> GetById(int Id)
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

        public async Task<IEnumerable<AddressDetails>> GetOnlyList()
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

        public async Task<AddressDetails> InsertData(AddressDetails AddressDetails)
        {
            try
            {
                var insert = await _repository.InsertData(AddressDetails);
                return insert;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public async Task<string> UpdateData(int id, AddressDetails AddressDetails)
        {
            try
            {
                var up = await _repository.UpdateData(id, AddressDetails);
                return up;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}
