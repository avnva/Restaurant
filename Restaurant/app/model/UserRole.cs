using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant;

public class UserRole
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Key]
    public int UserRoleId { get; set; } 
    public string UserRoleName { get; set; }
}
