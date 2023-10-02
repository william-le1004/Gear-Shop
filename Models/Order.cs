using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models
{
    public class Order
    {
        [Key]
        public int Id { get; set; }

        
        [DisplayName("Order Name")]
        public string Name { get; set; }

        [DisplayName("UserID")]
        public int UserId { get; set; }

        [ForeignKey("UserID")]
        [ValidateNever]
        public User User { get; set; }

        public IEnumerable<OrderDetails> OrderDetails { get; set; }

    }
}
