using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace Models
{
    public class OrderDetails
    {
        [Key]
        [DisplayName("Order Detail ID")]
        public int Id { get; set; }

        
        [DisplayName("Detail Name")]
        public string Name { get; set; }

        
        [DisplayName("Price")]
        public double Price { get; set; }

        [DisplayName("Quantity")]
        public double Quantity { get; set; }

        [DisplayName("OrderID")]
        public int OrderID { get; set; }

        [ForeignKey("OrderID")]
        [ValidateNever]
        public Order Order { get; set; }

        [DisplayName("ProductID")]
        public int ProductID { get; set; }

        [ForeignKey("ProductID")]
        [ValidateNever]
        public Product Product { get; set; }


    }
}
