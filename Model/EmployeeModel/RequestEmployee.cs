using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CRUDWEPAPI_EF.Model.EmployeeModel
{
    public class RequestEmployee
    {
        public string employeeName { get; set; }
        public DateTime promotedDate { get; set; }
        public string employeeDept { get; set; }

        [EmailAddress]
        public string employeeEmail { get; set; }
        public List<RequestAllowances> Allowances { get; set; }
    }

    public class RequestAllowances
    {
        public string allowanceType { get; set; }
        public decimal amount { get; set; } 
    }
}
