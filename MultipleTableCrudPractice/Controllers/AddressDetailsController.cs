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
    public class AddressDetailsController : ControllerBase
    {
        private readonly IAddressService _service;
        public AddressDetailsController(IAddressService service)
        {
            _service = service;
        }
        [HttpGet]
        [Route("GetAddressList")]
        public async Task<IActionResult> GetAddressDetailsList()
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
        [Route("GetAddressDetailsById/{AddressDetailsId}")]
        public async Task<IActionResult> GetAddressDetail(int AddressDetailsId)
        {
            try
            {
                var response = await _service.GetById(AddressDetailsId);
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
        [Route("PostAddressDetails")]
        public async Task<IActionResult> CreateAddressDetails(AddressDetails addressDetails)
        {

            try
            {
                var response = await _service.InsertData(addressDetails);
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



        [HttpDelete("DeleteById/{addressDetailsId}")]
        public async Task<IActionResult> DeleteAddressDetails(int addressDetailsId)
        {

            try
            {
                var response = await _service.DeleteData(addressDetailsId);
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
        [Route("PutAddressDetails/{addressDetailsId}")]
        public async Task<IActionResult> UpdateAddressDetails(int addressDetailsId, AddressDetails addressDetails)
        {
            try
            {
                var res = await _service.UpdateData(addressDetailsId, addressDetails);
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
