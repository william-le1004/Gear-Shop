using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities;

public class Cart
{
    public int Id { get; set; }
    [Required]
    public int UserID { get; set; }
    public bool IsDeleted { get; set; } = false;

    public ICollection<CartDetail> CartDetails { get; set; }
}
