

using CRUDWEPAPI_EF.Model.Requests;
using CRUDWEPAPI_EF.Repositories;
using Microsoft.AspNetCore.Mvc;
using System;

namespace CRUDWEPAPI_EF.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrder _order;
   //     private readonly ICustomer _customer; 

        public OrderController(IOrder order)
        {
            _order = order; 
        }

        [HttpPost]     
        [Route("SaveOrder")]
        public IActionResult SaveOrder([FromBody]  OrderSaveData order) 
        {
            try
            {
                var saveResult = _order.SaveOrder(order);
                return Ok(saveResult); 
            }
            catch(Exception)
            {
               return   BadRequest();
            }
           
        }
        [HttpGet]
        [Route("GetOrderList")]

        public IActionResult GetOrderList()
        {
            try { 
            var getOrderList = _order.GetOrderList();
            return Ok(getOrderList);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        [HttpGet]
        [Route("GetOrderListByID/{orderID}")]

        public IActionResult GetOrderListByID(int orderID)
        {
            try
            {
                var getOrderList = _order.GetOrderListByID(orderID);
                return Ok(getOrderList);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        [HttpPut]
        [Route("EditOrder")] 
        public IActionResult EditOrder([FromBody] OrderUpdateData orderUpdateData)
        {
            try
            {
                var editResult = _order.UpdateOrder(orderUpdateData);
                return Ok(editResult);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        [HttpDelete]
        [Route("DeleteOrder/{orderID}")]
        public IActionResult DeleteOrder(int orderID)
        {
            try
            {
                var deleteResult = _order.DeleteOrder(orderID);
                return Ok(deleteResult);
            }
            catch (Exception)
            {
                return BadRequest();
            }

        }

    }
}
