using CRUDWEPAPI_EF.Model;
using CRUDWEPAPI_EF.Model.Requests;
using CRUDWEPAPI_EF.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CRUDWEPAPI_EF.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomer _customer; 
        public CustomerController(ICustomer customer)
        {
            _customer = customer;
        }

        [HttpGet]
        [Route("GetCustomerList")]
        public IActionResult GetCustomerList()
        {
            try
            {
                var customer = _customer.GetCustomerList();
                if (customer == null) return NotFound();
                return Ok(customer);
            }
            catch (Exception)
            {
                return BadRequest(); 
            }
        }

        [HttpPost]
        [Route("SaveCustomer")] 
        public IActionResult SaveCustomer([FromBody] CustomerSave customerModel)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(customerModel);

                var model = _customer.SaveCustomer(customerModel); 
               return Ok(model);
                //return StatusCode(201); 
            }
            catch (Exception)
            {
                return BadRequest();
            }
           
        }


        [HttpPut]
        [Route("UpdateCustomer")]
        public IActionResult UpdateCustomer([FromBody] CustomerEdit customerModel) 
        {
            try
            {
                var model = _customer.UpdateCustomer(customerModel);    
                return Ok(model);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }


        [HttpGet]
        [Route("FindByCustomerID/{customerID}")]
        public IActionResult FindByCustomerID(int customerID)
        {
            try
            {
                var model = _customer.FindByCustomerID(customerID);
                return Ok(model);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }


        [HttpDelete]
        [Route("DeleteCustomer/{customerID}")]
        public IActionResult DeleteCustomer(int customerID)
        {
            try
            {
                var model = _customer.DeleteCustomer(customerID); 
                return Ok(model);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

    }
}
