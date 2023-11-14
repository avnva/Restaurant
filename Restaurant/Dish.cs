using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant;

public class Dish
{
    public int DishID { get; set; }
    public int GroupID { get; set; }
    public string DishName { get; set; }
    public decimal DishCost { get; set; }
    public float OutputWeight { get; set; }
    public string CookingTechnology { get; set; }
    public byte[] Photo { get; set; }
}
