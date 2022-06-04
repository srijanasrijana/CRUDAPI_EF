using CRUDWEPAPI_EF.Config;
using CRUDWEPAPI_EF.Model;
using CRUDWEPAPI_EF.Model.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CRUDWEPAPI_EF.Repositories
{
    public  interface IOrder
    {
        ResponseModel SaveOrder(OrderSaveData orderModel);
        Order MapSaveOrder(OrderSaveData orderModel);
        OrderListResponse OrderResponse(Order orderResponse);

        ResponseModel GetOrderList();
        ResponseModel GetOrderListByID(int orderId);

        ResponseModel UpdateOrder(OrderUpdateData orderUpdateData);

        ResponseModel DeleteOrder(int OrderId);

    }
}
