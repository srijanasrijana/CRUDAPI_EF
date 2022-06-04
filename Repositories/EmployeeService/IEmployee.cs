using CRUDWEPAPI_EF.Model;
using CRUDWEPAPI_EF.Model.EmployeeModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CRUDWEPAPI_EF.Repositories.EmployeeService
{
   public interface IEmployee
    {
        ResponseModel SaveEmployee(RequestEmployee employee);
        Employee RequestEmployee(RequestEmployee requestEmployee);
        ResponseModel GetEmployee();
        ResponseModel GetEmployeeByID(int employeeID); 

  
    }
}
