using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MultipleTableCrudPractice.Data.Entities;
using MultipleTableCrudPractice.Service.MultipleCrudService;
using System;
using System.Threading.Tasks;

namespace MultipleTableCrudPractice.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SalaryController : ControllerBase
    {
        private readonly ISalaryService _service;
        public SalaryController(ISalaryService service)
        {
            _service = service;
        }
        [HttpGet]
        [Route("GetSalaryList")]
        public async Task<IActionResult> GetSalaryList()
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
        [Route("GetSalaryById/{SalaryId}")]
        public async Task<IActionResult> SalaryDetails(int SalaryId)
        {
            try
            {
                var response = await _service.GetById(SalaryId);
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
        [Route("PostSalary")]
        public async Task<IActionResult> CreateSalary(Salary salary)
        {

            try
            {
                var response = await _service.InsertData(salary);
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



        [HttpDelete("DeleteById/{SalaryId}")]
        public async Task<IActionResult> DeleteSalary(int salaryId)
        {

            try
            {
                var response = await _service.DeleteData(salaryId);
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
        [Route("PutSalary/{SalaryId}")]
        public async Task<IActionResult> UpdateSalary(int salaryId, Salary salary)
        {
            try
            {
                var res = await _service.UpdateData(salaryId, salary);
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
