using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CRUDWEPAPI_EF.Model
{
    public class ResponseModel
    {

        public bool IsSuccess { get; set; }      
        public string Messsage { get; set; }
        public string ResponseCode { get; set; }
        public object ResponseData { get; set; }  
     

       
    }
}
