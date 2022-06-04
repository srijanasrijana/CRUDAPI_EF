using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CRUDWEPAPI_EF.Model.Requests
{
    public class OrderEdit
    {
        [Key]
        public int OrderId { get; set; }

        public DateTime DateTime { get; set; }

        [Required]
        [Column(TypeName = "decimal(18,2)")]
        public decimal Amount { get; set; }
    }
}
