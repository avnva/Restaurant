using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Restaurant.app.model;

public class OrdersDishes
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Key]
    [Column(Order = 1)]
    public int DishID { get; set; }
    [Key]
    [Column(Order = 2)]
    public int OrderID { get; set; }
    public double Quantity { get; set; }

    [ForeignKey(nameof(DishID))]
    public Dish Dish { get; set; }

    [ForeignKey(nameof(OrderID))]
    public Order Order { get; set; }
}
