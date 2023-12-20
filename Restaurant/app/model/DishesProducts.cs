using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.app.model
{
    public class DishesProducts
    {
        public int DishID { get; set; }
        public int ProductID { get; set; }
        public int Quantity { get; set; }

        [ForeignKey(nameof(DishID))]
        public Dish Dish { get; set; }

        [ForeignKey(nameof(ProductID))]
        public Product Product { get; set; }
    }
}
