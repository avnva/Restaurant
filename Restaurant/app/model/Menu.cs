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
    public Menu()
    {
        DishInMenuId = null;
    }

    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Key]
    [Column("DishInMenuID")]
    public int? DishInMenuId { get; set; }
    [Column("DishID")]
    public int DishId { get; set; }
    [Column("StatusID")]
    public int StatusId { get; set; }
    public string Comment { get; set; }
}
