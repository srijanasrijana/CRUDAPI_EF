using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CRUDWEPAPI_EF.Model.Requests
{
    public class CustomerSave
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Address { get; set; }

  
        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress]
        public string Email { get; set; }

        public List<OrderSave> Orders { get; set; } 
    }

}
