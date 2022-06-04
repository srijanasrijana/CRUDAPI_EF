using CRUDWEPAPI_EF.Config;
using CRUDWEPAPI_EF.Model;
using CRUDWEPAPI_EF.Model.EmployeeModel;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CRUDWEPAPI_EF.Repositories.EmployeeService
{
    public class EmployeeServices : IEmployee
    {

        private readonly ApplicationDBContext _DBContext;

        public EmployeeServices(ApplicationDBContext DBContext)
        {
            _DBContext = DBContext;
        }

        //Method to Save Employee 
        public ResponseModel SaveEmployee(RequestEmployee employee)
        {
            ResponseModel response = new ResponseModel(); 
            try
            {
                Employee employeeData = RequestEmployee(employee); 
               // _DBContext.Add<Employee>(employee);
                _DBContext.Employee.Add(employeeData);
                _DBContext.SaveChanges();
                
                response.ResponseCode = "200";
                response.IsSuccess = true;
                response.Messsage = "Employee Save Sucessfully";
                response.ResponseData = employeeData;
             
            }
            catch(Exception ex)
            {
                response.IsSuccess = false;
                response.Messsage = "Error" + ex.Message;
                response.ResponseCode = "500";
            }
            return response;
        }

        public Employee RequestEmployee(RequestEmployee requestEmployee)
        {
            Employee employee = new Employee();
            employee.employeeName = requestEmployee.employeeName;
            employee.promotedDate = requestEmployee.promotedDate;
            employee.employeeDept = requestEmployee.employeeDept;
            employee.employeeEmail = requestEmployee.employeeEmail;

            List<Allowances> allowances = new List<Allowances>();

            foreach(RequestAllowances allowanceData in requestEmployee.Allowances) 
            {
                Allowances requestAllowances = new Allowances();
                requestAllowances.allowanceType = allowanceData.allowanceType;
                requestAllowances.amount = allowanceData.amount;
                allowances.Add(requestAllowances);
            }
           
            employee.Allowances = allowances;
            return employee;
        }

        //Method To Fetch Employee List
        public ResponseModel GetEmployee()
        {
            ResponseModel response = new ResponseModel();
            try
            {

               // var employeeList = _DBContext.Employee.Include(e => e.Allowances).ToList(); 

                var employeeList = _DBContext.Employee.Include("Allowances")
                                         .Select(e => new ResponseEmployee
                                         {
                                             employeeName = e.employeeName,
                                             employeeDept = e.employeeDept,
                                             employeeEmail = e.employeeEmail,
                                             promotedDate = e.promotedDate,
                                             Allowances = e.Allowances.Select
                                                          (a => new ResponseAllowances
                                                          {
                                                              allowanceType = a.allowanceType,
                                                              amount = a.amount
                                                          }).ToList()
                                         }).ToList();


                response.ResponseCode = "200";
                response.IsSuccess = true;
                response.Messsage = "Data Fetch Sucessfully";
                response.ResponseData = employeeList;

            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Messsage = "Error" + ex.Message;
                response.ResponseCode = "500";
            }
            return response;
        }

        public ResponseModel GetEmployeeByID(int employeeID)
        {
            ResponseModel response = new ResponseModel();
            try
            {
                var employeeListByID = _DBContext.Employee
                                      .Where(e => e.employeeID == employeeID)
                                      .Include(e => e.Allowances)
                                      .Select(e => new ResponseEmployee
                                      {
                                          employeeName = e.employeeName,
                                          employeeEmail = e.employeeEmail,
                                          promotedDate = e.promotedDate,
                                          employeeDept = e.employeeDept,
                                          Allowances = e.Allowances.Select(
                                                  a => new ResponseAllowances
                                                  {
                                                      allowanceType = a.allowanceType,
                                                      amount = a.amount
                                                  }).ToList()

                                      }).FirstOrDefault();


                response.ResponseCode = "200";
                response.IsSuccess = true;
                response.Messsage = "Data Fetch Sucessfully";
                response.ResponseData = employeeListByID;
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Messsage = "Error" + ex.Message;
                response.ResponseCode = "500";
            }
            return response;
        }
    }
}
