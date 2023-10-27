using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities;

public class CartItem
{
    public Product product { get; set; }
    public int Quantity { get; set; }
}
