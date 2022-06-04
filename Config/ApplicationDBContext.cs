using CRUDWEPAPI_EF.Model;
using CRUDWEPAPI_EF.Model.EmployeeModel;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CRUDWEPAPI_EF.Config
{
    public class ApplicationDBContext : DbContext
    {
       public ApplicationDBContext()
        {

        }
        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options)
            : base(options)
        {
        }
     
        public  DbSet<Customer> Customer { get; set; }
        public  DbSet<Order> Order { get; set; }
        public DbSet<Employee> Employee { get; set; }
        public DbSet<Allowances> Allowance { get; set; } 

    }
}
