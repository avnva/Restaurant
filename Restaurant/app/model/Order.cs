using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Restaurant.app.model;

namespace Restaurant;

public class Order
{
    public int OrderID { get; set; }
    public DateTime OrderDate { get; set; }
    public decimal OrderCost { get; set; }

    [NotMapped]
    public ICollection<Dish> OrderedDishes { get; set; }
}
