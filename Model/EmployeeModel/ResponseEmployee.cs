using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CRUDWEPAPI_EF.Model.EmployeeModel
{
    public class ResponseEmployee
    {
        public string employeeName { get; set; }
        public DateTime promotedDate { get; set; }
        public string employeeDept { get; set; }
        public string employeeEmail { get; set; }
        public List<ResponseAllowances> Allowances { get; set; }
    }

    public class ResponseAllowances
    {
        public string allowanceType { get; set; }
        public decimal amount { get; set; }
    }
}
