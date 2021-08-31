using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MultipleTableCrudPractice.Data.Entities;
using MultipleTableCrudPractice.Service.MultipleCrudService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MultipleTableCrudPractice.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BankAccountController : ControllerBase
    {
        private readonly IBankAccountService _service;
        public BankAccountController(IBankAccountService service)
        {
            _service = service;
        }
        [HttpGet]
        [Route("GetBankAccountList")]
        public async Task<IActionResult> GetBankAccountList()
        {
            try
            {
                var response = await _service.GetOnlyList();
                if (response != null)
                {
                    return Ok(response);
                }
                return StatusCode(StatusCodes.Status204NoContent);
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

        // GET: Company/Details/5
        [HttpGet]
        [Route("GetBankAccountById/{BankAccountId}")]
        public async Task<IActionResult> GetBankAccountById(int BankAccountId)
        {
            try
            {
                var response = await _service.GetById(BankAccountId);
                if (response != null)
                {
                    return Ok(response);
                }
                return StatusCode(StatusCodes.Status204NoContent);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        [HttpPost]
        [Route("PostBankAccount")]
        public async Task<IActionResult> CreateBankAccount(BankAccount bankAccount)
        {

            try
            {
                var response = await _service.InsertData(bankAccount);
                if (response != null)
                {
                    return Ok(response);
                }
                return StatusCode(StatusCodes.Status204NoContent);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }



        [HttpDelete("DeleteById/{bankAccountId}")]
        public async Task<IActionResult> DeleteBankAccount(int bankAccountId)
        {

            try
            {
                var response = await _service.DeleteData(bankAccountId);
                if (response != null)
                {
                    return Ok(response);
                }
                return StatusCode(StatusCodes.Status204NoContent);
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

        [HttpPut]
        [Route("PutBankAccount/{bankAccountId}")]
        public async Task<IActionResult> UpdateBankAccount(int bankAccountId, BankAccount bankAccount)
        {
            try
            {
                var res = await _service.UpdateData(bankAccountId, bankAccount);
                if (res != null)
                {
                    return Ok(res);
                }
                return StatusCode(StatusCodes.Status204NoContent);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
