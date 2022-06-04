using CRUDWEPAPI_EF.Model.EmployeeModel;
using CRUDWEPAPI_EF.Repositories.EmployeeService;
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
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployee _employee;
        public EmployeeController (IEmployee employee)
        {
            _employee = employee;
        }

        [HttpPost]
        [Route("SaveEmployee")]
        public IActionResult SaveEmployee([FromBody] RequestEmployee employee)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(employee);
                
                var response = _employee.SaveEmployee(employee);
                return Ok(response);
            }
            catch
            {
                return BadRequest();
            }
        }


        [HttpGet]
        [Route("GetEmployee")]
        public IActionResult GetEmployee()
        {
            try
            {
                var response = _employee.GetEmployee();
                return Ok(response);
            }
            catch
            {
                return BadRequest();
            }
        }
        [HttpGet]
        [Route("GetEmployeeByID/{employeeID}")]
        public IActionResult GetEmployeeByID(int employeeID)
        {
            try
            {
                var response = _employee.GetEmployeeByID(employeeID);
                return Ok(response);
            }
            catch
            {
                return BadRequest();
            }
        }
    }
}
