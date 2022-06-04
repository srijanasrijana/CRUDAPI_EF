using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CRUDWEPAPI_EF.Model
{
    public class Customer
    {
        [Key]
        public int CustomerID { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Address { get; set; }

        [Display(Name = "Email Address")]
        [Required(ErrorMessage = "Email is required.")]
        [DataType(DataType.EmailAddress, ErrorMessage = "E-mail is not valid")]
        public string Email { get; set; }  

        public List<Order> Orders { get; set; }

       
    }
}
