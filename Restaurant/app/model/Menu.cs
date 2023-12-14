using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant;

public class Menu
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Key]
    public int DishInMenuId { get; set; }
    public int DishId { get; set; }
    public int StatusId { get; set; }
    public string Comment { get; set; }
}
