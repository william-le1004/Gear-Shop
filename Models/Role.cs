using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class Role
    {
        [Key]
        public int Id { get; set; }

        [DisplayName("Role Name")]
        public string RoleName { get; set; }

        [DisplayName("Permission")]
        public bool IsAdmin { get; set; }
    }
}
