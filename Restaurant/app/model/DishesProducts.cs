using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Restaurant.app.model;

public class DishesProducts
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Key]
    [Column(Order = 1)]
    public int DishID { get; set; }
    [Key]
    [Column(Order = 2)]
    public int ProductID { get; set; }
    public double Quantity { get; set; }

    [ForeignKey(nameof(DishID))]
    public Dish Dish { get; set; }

    [ForeignKey(nameof(ProductID))]
    public Product Product { get; set; }
}
