using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CRUDWEPAPI_EF.Model.EmployeeModel
{
    public class Allowances
    {
        [Key]
        public int employeeAllowancesID { get; set; }
        public int employeeID { get; set; }
        public string allowanceType { get; set; } 
        public decimal amount { get; set; }
        public Employee Employee { get; set; }
    }
}
