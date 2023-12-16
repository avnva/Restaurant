using System.ComponentModel.DataAnnotations.Schema;
using System.Net.Sockets;

namespace Restaurant.app.model;

public class Dish
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int DishID { get; set; }
    public int GroupID { get; set; }
    public string DishName { get; set; }
    public decimal DishCost { get; set; }
    public double OutputWeight { get; set; }
    public string CookingTechnology { get; set; }
    public string? Photo { get; set; }
}
