using ClosedXML.Excel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MultipleTableCrudPractice.Data.DTOs;
using MultipleTableCrudPractice.Data.Entities;
using MultipleTableCrudPractice.Data.ViewModels;
using MultipleTableCrudPractice.Service.MultipleCrudService;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace MultipleTableCrudPractice.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeDetailsController : ControllerBase
    {
        private readonly IEmployeeDetailsService _service;
        public EmployeeDetailsController(IEmployeeDetailsService service)
        {
            _service = service;
        }
        [HttpGet]
        [Route("GetEmployeeDetailList")]
        public async Task<IActionResult> GetEmployeeDetailsList()
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
        [Route("GetEmployeeDetailsById/{EmployeeDetailsId}")]
        public async Task<IActionResult> GetEmployeeDetailById(int EmployeeDetailsId)
        {
            try
            {
                var response = await _service.GetById(EmployeeDetailsId);
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
        #region Single Entity Insert
        [HttpPost]
        [Route("PostEmployeeDetails")]
        public async Task<IActionResult> CreateEmployeeDetails(EmployeeDetails employeeDetails)
        {

            try
            {
                var response = await _service.InsertData(employeeDetails);
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
        #endregion
        #region Insert data into two entities using viewmodel (two objects)
        [HttpPost]
        [Route("InsertEmployeeAddress")]
        public async Task<IActionResult> CreateEmployeeDetailsVM(EmployeeAddressVM employeeAddressVM)
        {

            try
            {
                var response = await _service.InsertDataVm(employeeAddressVM);
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
        #endregion

        #region Insert data into two entities (object and list together)
        [HttpPost]
        [Route("InsertDataObjectWithListVm")]
        public async Task<IActionResult> CreateEmployeeDetailObjectWithList(EmployeeDetails employeeAddressVM)
        {

            try
            {
                var response = await _service.InsertDataObjectWithListVm(employeeAddressVM);
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

        #endregion

        [HttpDelete("DeleteById/{employeeDetailsId}")]
        public async Task<IActionResult> DeleteEmployeeDetails(int employeeDetailsId)
        {

            try
            {
                var response = await _service.DeleteData(employeeDetailsId);
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
        [Route("PutEmployeeDetails/{employeeDetailsId}")]
        public async Task<IActionResult> UpdateEmployeeDetails(int employeeDetailsId, EmployeeDetails employeeDetails)
        {
            try
            {
                var res = await _service.UpdateData(employeeDetailsId, employeeDetails);
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
        [HttpPost]
        [Route("PostEmployeeExcelAsync")]
        public async Task<IActionResult> ExcelAsync()
        {
            using (var workbook = new XLWorkbook())
            {
                var worksheet = workbook.Worksheets.Add("Employee");
                var currentRow = 1;
                worksheet.Cell(currentRow, 1).Value = "EmployeeId";
                worksheet.Cell(currentRow, 2).Value = "EmployeeName";
                worksheet.Cell(currentRow, 3).Value = "Designation";
                var response = await _service.GetOnlyList();
                foreach (var user in response)
                {
                    currentRow++;
                    worksheet.Cell(currentRow, 1).Value = user.EmployeeName;
                    worksheet.Cell(currentRow, 2).Value = user.EmployeeName;
                    worksheet.Cell(currentRow, 3).Value = user.Designation;
                }

                using (var stream = new MemoryStream())
                {
                    workbook.SaveAs(stream);
                    var content = stream.ToArray();

                    return File(
                        content,
                        "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                        "users.xlsx");
                }
            }
        }
        #region  Insert Data One to One
        [HttpPost]
        [Route("EmployeePostDto")]
        public async Task<IActionResult> EmployeePostDto(EmployeeDto dto)
        {

            try
            {
                var res = await _service.InsertDataDTO(dto);
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
        #endregion
        #region Insert Data One to Many
        [HttpPost]
        [Route("InsertManyDataDTO")]
        public async Task<IActionResult> InsertManyData(ManyEmployeeDto dto)
        {

            try
            {
                var response = await _service.InsertDataMany(dto);
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
        #endregion

        #region  Update Data One to One
        [HttpPut]
        [Route("UpdateEmployeeVM")]
        public async Task<IActionResult> EmployeeVMUpdate(EmployeeAddressVM vm)
        {

            try
            {
                var res = await _service.UpdateDataVm(vm);
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
        #endregion

    }
}
