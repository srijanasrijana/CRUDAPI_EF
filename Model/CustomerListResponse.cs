using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CRUDWEPAPI_EF.Model
{
    public class CustomerListResponse
    {
        public int CustomerID { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public List<OrderListResponse> Orders { get; set; }
    }

    public class OrderListResponse
    {
        public int OrderId { get; set; }
        public decimal Amount { get; set; }
        public DateTime DateTime { get; set; }  

    }

    public class OrderDataResponse  
    {

        public decimal Amount { get; set; }
        public DateTime DateTime { get; set; }

    }
}
