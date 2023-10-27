using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities;

public class OrderDetail
{
    public int Id { get; set; }

    [Required]
    public double Price { get; set; }

    [Required]
    public int Quantity { get; set; }
  
    [Required]
    public int OrderID { get; set; }
   
    [ValidateNever]
    public Order Order { get; set; }
   
    [Required]
    public int ProductID { get; set; }

    [ValidateNever]
    public Product Product { get; set; }



}
