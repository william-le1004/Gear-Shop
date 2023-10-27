using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    public class Order
    {
      
        public int Id { get; set; }

        public DateTime CreateDate { get; set; } = DateTime.UtcNow;

        [DisplayName("UserID")]
        public int UserID { get; set; }

        public bool IsDeleted { get; set; } = false;

        [ValidateNever]
        public User User { get; set; }

        public IEnumerable<OrderDetail> OrderDetails { get; set; }

    }
}
