using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities;

public class CartDetail
{
    public int Id { get; set; }
    [Required]
    public int CartId { get; set; }
    [Required]
    public int ProductID { get; set; }
    [Required]
    public int Quantity { get; set; }
    [Required]
    public double UnitPrice { get; set; }
    public Product Product { get; set; }
    public Cart ShoppingCart { get; set; }
}
