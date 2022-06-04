using CRUDWEPAPI_EF.Config;
using CRUDWEPAPI_EF.Model;
using CRUDWEPAPI_EF.Model.Requests;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CRUDWEPAPI_EF.Repositories
{
    public class CustomerService : ICustomer
    {

        private readonly ApplicationDBContext _context;
        public CustomerService(ApplicationDBContext context)
        {
            _context = context;
        }
        public ResponseModel GetCustomerList() 
        {
            ResponseModel model = new ResponseModel();
            List<CustomerListResponse> cusList;
            try
            {
               // cusList = _context.Set<Customer>().ToList();  //to get list from one table with set List and ToList() fucntion


                //To get list from one to many table  use  Include and Select with ToList() function
                cusList = _context.Customer
                                 .Include("Orders").Select(c =>
                                        new CustomerListResponse { 
                                            CustomerID = c.CustomerID, 
                                            Name = c.Name,
                                            Email = c.Email, 
                                            Orders = c.Orders.Select(
                                                    o => new OrderListResponse
                                                    {
                                                        OrderId = o.OrderId,
                                                        Amount = o.Amount
                                                    }
                                               ).ToList()
                                        }
                                      ).ToList();


                model.ResponseData = cusList;
                model.IsSuccess = true;
                model.ResponseCode = "200";
                model.Messsage = "Customer Data Fetched Successfully";
            }
            catch (Exception ex)
            {
                model.ResponseCode = "500";
                model.IsSuccess = false;
                model.Messsage = "Error : " + ex.Message;
            }
            return model; 
        }
        public ResponseModel FindByCustomerID(int customerID)
        {

            ResponseModel response = new ResponseModel();
            try
            {
                //To get list from ID of Customer 
                //Customer customer = _context.Customer
                //    .Where(c => c.CustomerID == customerID)
                //    .Include(c => c.Orders)
                //    .FirstOrDefault();


                //To get list from one to many entity 

                CustomerListResponse customer = _context.Customer
                    .Where(c => c.CustomerID == customerID)
                    .Include("Orders").Select(c =>
                                       new CustomerListResponse
                                       {
                                           CustomerID = c.CustomerID,
                                           Name = c.Name,
                                           Email = c.Email,
                                           Orders = c.Orders.Select(
                                                   o => new OrderListResponse
                                                   {
                                                       OrderId = o.OrderId,
                                                       Amount = o.Amount
                                                   }
                                              ).ToList()
                                       }
                                      ).FirstOrDefault();

                    response.ResponseCode = "200";
                    response.Messsage = "Data Fetch Sucessfully";
                    response.IsSuccess = true;
                    response.ResponseData = customer;

               
            }
            catch (Exception ex)
            {
                response.ResponseCode = "500";
                response.Messsage = "Error:" + ex.Message;
                response.IsSuccess = false;
              
            }
            return response;
        }

        public ResponseModel SaveCustomer(CustomerSave customerSaveModel) 
        {
            ResponseModel model = new ResponseModel();
            try
            {
                // Call mapCustomerSaveToCustomer fucntion with input and output in Customer
                Customer customer = mapCustomerSaveToCustomer(customerSaveModel);
                _context.Add<Customer>(customer);
                model.ResponseCode = "201";
                model.Messsage = "Customer Inserted Successfully";
                _context.SaveChanges();
                model.IsSuccess = true;
                model.ResponseData = mapCustomerListResponse(customer);
            }
            catch (Exception ex)
            {
                model.ResponseCode = "500";
                model.IsSuccess = false;
                model.Messsage = "Error : " + ex.Message;
            }
            return model;
        }

        public ResponseModel UpdateCustomer(CustomerEdit customer)
        {

            ResponseModel model = new ResponseModel();
            try
            {
                //To get exitst Data by ID with one to many realtionship
                var existingCustomer = _context.Customer.Where(c => c.CustomerID == customer.CustomerID )
                                          .Include(c => c.Orders)
                                                    .FirstOrDefault();
                //Check Existing data 
                if (existingCustomer  != null)
                {
                    //set new data in existing data
                    existingCustomer.Name = customer.Name;
                    existingCustomer.Email = customer.Email;
                    existingCustomer.Address = customer.Address;


                    //set list of new order in exisitn data 
                    List<Order> orders = new List<Order>();
                    foreach (OrderEdit order in customer.Orders)
                    {
                        Order orderData = new Order();
                        orderData.OrderId = order.OrderId;
                        orderData.CustomerID = existingCustomer.CustomerID;
                        orderData.Customer = existingCustomer;
                        orderData.DateTime = order.DateTime;
                        orderData.Amount = order.Amount;

                        orders.Add(orderData);
                    }
                    existingCustomer.Orders = orders;

                    // _context.Update<Customer>(customerData);
                    _context.Update<Customer>(existingCustomer);
                    _context.SaveChanges();
                    model.IsSuccess = true;
                    model.ResponseCode = "200";
                    model.Messsage = "Data Update sucessfully";
                    model.ResponseData = existingCustomer;
                }
                else
                {
                    model.IsSuccess = false;
                    model.ResponseCode = "500";
                    model.Messsage = "Customer Data Not Found";
                }

               
            }
            catch(Exception ex)
            {
                model.IsSuccess = false;
                model.Messsage = "Error : " + ex.Message;
            }
            return model;
        }

        public Customer mapCustomerSaveToCustomer(CustomerSave customerSaveModel)
        {
            //It takes CustomerSave  input and Customer Output
            Customer customer = new Customer();
            customer.Name = customerSaveModel.Name;
            customer.Address = customerSaveModel.Address;
            customer.Email = customerSaveModel.Email;
            List<Order> orders = new List<Order>();
            foreach(OrderSave orderSave in customerSaveModel.Orders)
            {
                Order order = new Order();
                order.DateTime = orderSave.DateTime;
                order.Amount = orderSave.Amount;
                orders.Add(order);
            }
            customer.Orders = orders;
            return customer;
        }

   
        //To show Response object format in JSON of one to many relationship
        public CustomerListResponse mapCustomerListResponse(Customer Customer)
        {
            CustomerListResponse customerListResponse = new CustomerListResponse();
            customerListResponse.CustomerID = Customer.CustomerID;
            customerListResponse.Name = Customer.Name;
            customerListResponse.Email = Customer.Email;

            List<OrderListResponse> orderListResponses  = new List<OrderListResponse>();
            foreach (Order order in Customer.Orders)
            {
                OrderListResponse orderListResponse = new OrderListResponse();
                orderListResponse.OrderId = order.OrderId;
                orderListResponse.Amount = order.Amount;
                orderListResponse.DateTime = order.DateTime;
                orderListResponses.Add(orderListResponse); 
            }

            customerListResponse.Orders = orderListResponses;
            return customerListResponse;
        }


        //Delete Customer Data by ID 
        public ResponseModel DeleteCustomer(int customerID)
        {
            ResponseModel response = new ResponseModel();
            try
            {
                if (customerID != 0)
                {
                    //To Delete First Data
                   // var customer = _context.Customer.OrderBy(c => c.CustomerID).Include(c => c.Orders).First();
                   //To delete selected data by Id
                    var customer = _context.Customer
                                   .Where(c => c.CustomerID == customerID)
                                   .Include(c => c.Orders)
                                   .FirstOrDefault();
                    _context.Remove(customer);
                    _context.SaveChanges();
                    response.ResponseCode = "200";
                    response.Messsage = "Delete Successfully";
                    response.IsSuccess = true;
                    response.ResponseData = mapCustomerListResponse(customer);
                  

                }
                else
                {
                    response.ResponseCode = "500";
                    response.Messsage = "Customer Data Not Found";
                    response.IsSuccess = false;
                }
            }
            catch (Exception ex)
            {
                response.Messsage = "Error:" + ex.Message;
                response.IsSuccess = false;
            }
            return response;
        
    }
        //To fetch one data object of Customer by ID
        public Customer GetCustomerDataById(int customerId)
        {
            var customer = _context.Customer.Where((Customer c) => c.CustomerID == customerId)
                                          .Include((Customer c) => c.Orders)     //List of Order 
                                          .FirstOrDefault();
            return customer;        
        }


      
    }
}
