using System.ComponentModel.DataAnnotations.Schema;
using System.Net.Sockets;

namespace Restaurant.app.model;

public class Dish
{
    public Dish()
    {
        DishID = null;
        DishName = string.Empty;
        DishCost = 0.0m;
        OutputWeight = 0.0;
        CookingTechnology = string.Empty;
        Photo = null;
        DishGroup = new DishGroup();
        Status = new Status();
    }

    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int? DishID { get; set; }
    public int GroupID { get; set; }
    public string DishName { get; set; }
    public decimal DishCost { get; set; }
    public double OutputWeight { get; set; }
    public string CookingTechnology { get; set; }
    public string? Photo { get; set; }
    public DishGroup DishGroup { get; set; }
    public Status Status { get; set; }
}
