using CRUDWEPAPI_EF.Config;
using CRUDWEPAPI_EF.Model;
using CRUDWEPAPI_EF.Model.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CRUDWEPAPI_EF.Repositories
{
    public class OrderService : IOrder 
    {
        private readonly ApplicationDBContext _context;
        private readonly ICustomer _customerSerivce ; 
        public OrderService(ApplicationDBContext context, ICustomer customerSerivce)
        {
            _context = context;
            _customerSerivce = customerSerivce;
        }


        public ResponseModel SaveOrder(OrderSaveData orderModel)
        {
            ResponseModel model = new ResponseModel();
            try
            {
                Order orderData = MapSaveOrder(orderModel);
                if (orderData != null)
                { 
                _context.Add<Order>(orderData);
                _context.SaveChanges();

                model.IsSuccess = true;
                model.ResponseCode = "200";
                model.Messsage = "Save Order  Sucessfully";
                model.ResponseData = OrderResponse(orderData);
                }
                else
                {
                    model.ResponseCode = "500";
                    model.IsSuccess = false;
                    model.Messsage = "Failed to Save";
                }
            }
            catch (Exception ex)
            {
                model.ResponseCode = "500";
                model.IsSuccess = false;
                model.Messsage = "Error" + ex.Message;
            }
            return model;
        }

        public Order MapSaveOrder(OrderSaveData orderSaveData)
        {

            Order orders = new Order();
            orders.CustomerID = orderSaveData.CustomerID;
            orders.Amount = orderSaveData.Amount;
            orders.DateTime = orderSaveData.DateTime;
            orders.Customer = _customerSerivce.GetCustomerDataById(orderSaveData.CustomerID);
            return orders;

        }
       public OrderListResponse OrderResponse(Order OrderResponse)
        {
            OrderListResponse orderListResponse = new OrderListResponse();
            orderListResponse.OrderId = OrderResponse.OrderId;
            orderListResponse.DateTime = OrderResponse.DateTime;
            orderListResponse.Amount = OrderResponse.Amount;
            return orderListResponse;
        }

        public ResponseModel GetOrderList()
        {
            ResponseModel model = new ResponseModel();
            try
            {
                var orderList = _context.Order
                            .Select(o => new OrderListResponse()
                            {
                                OrderId=o.OrderId,
                                Amount = o.Amount,
                                DateTime = o.DateTime
                            })
                            .ToList(); 
                model.IsSuccess = true;
                model.ResponseCode = "200";
                model.Messsage = "Data Fetched Sucessfully";
                model.ResponseData = orderList;
            }
            catch (Exception ex)
            {
                model.ResponseCode = "500";
                model.IsSuccess = false;
                model.Messsage = "Error" + ex.Message;
            }
            return model;
        }

        public ResponseModel GetOrderListByID(int orderId) 
        {
            ResponseModel model = new ResponseModel();
            try
            {
                var orderList = _context.Order
                             .Where(o => o.OrderId == orderId)
                            .Select(o => new OrderDataResponse()
                            {
                                Amount = o.Amount,
                                DateTime = o.DateTime
                            })
                            .SingleOrDefault();
                model.IsSuccess = true;
                model.ResponseCode = "200";
                model.Messsage = "Data Fetched Sucessfully";
                model.ResponseData = orderList;
            }
            catch (Exception ex)
            {
                model.ResponseCode = "500";
                model.IsSuccess = false;
                model.Messsage = "Error" + ex.Message;
            }
            return model;
        }
        public ResponseModel UpdateOrder(OrderUpdateData orderUpdateData)
        {
            ResponseModel model = new ResponseModel();

            try
            {

                var orderList = _context.Order
                           .Where(o => o.OrderId == orderUpdateData.OrderID)
                           .FirstOrDefault();

             //   var orderList = _context.Order.FirstOrDefault();
                if (orderList != null)
                {
                    orderList.OrderId = orderUpdateData.OrderID;
                    orderList.CustomerID = orderUpdateData.CustomerID;
                    orderList.Amount = orderUpdateData.Amount;
                    orderList.DateTime = orderUpdateData.DateTime;
                    orderList.Customer=  _customerSerivce.GetCustomerDataById(orderUpdateData.CustomerID);

                    _context.Update<Order>(orderList);
                    _context.SaveChanges();

                    model.IsSuccess = true;
                    model.ResponseCode = "200";
                    model.Messsage = "Order Update Sucessfully";
                    model.ResponseData = orderList;
                }
                else
                {
                    model.ResponseCode = "500";
                    model.IsSuccess = false;
                    model.Messsage = "Data Not Found";
                }
            }
            catch (Exception ex)
            {
                model.ResponseCode = "500";
                model.IsSuccess = false;
                model.Messsage = "Error" + ex.Message;
            }
            return model;
        }

        public ResponseModel DeleteOrder(int OrderId)
        {
            ResponseModel model = new ResponseModel();
            try
            {
                var orderData = _context.Order
                            .Where(o => o.OrderId == OrderId)
                            .FirstOrDefault();

                if(orderData != null)
                { 
                 _context.Remove(orderData);
                _context.SaveChanges();

                model.IsSuccess = true;
                model.ResponseCode = "200";
                model.Messsage = "Order Delete Sucessfully";
                model.ResponseData = OrderResponse(orderData);
                }
                else
                {
                    model.ResponseCode = "500";
                    model.IsSuccess = false;
                    model.Messsage = "Failed to  Delete";
                }
            }
            catch(Exception ex)
            {
                model.ResponseCode = "500";
                model.IsSuccess = false;
                model.Messsage = "Error" + ex.Message;
            }
            return model;
        }

       
    }
}
