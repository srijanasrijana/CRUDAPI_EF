using CRUDWEPAPI_EF.Model;
using CRUDWEPAPI_EF.Model.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CRUDWEPAPI_EF.Repositories
{
  public  interface ICustomer 
    {
        ResponseModel GetCustomerList();
        ResponseModel SaveCustomer(CustomerSave customerSaveModel);  
        Customer mapCustomerSaveToCustomer(CustomerSave customerSave);
        CustomerListResponse mapCustomerListResponse(Customer  Customer);
        ResponseModel UpdateCustomer(CustomerEdit customer);
        ResponseModel FindByCustomerID(int customerID);
        ResponseModel DeleteCustomer(int customerID);
        Customer GetCustomerDataById(int customerId); 
       

    }
}
