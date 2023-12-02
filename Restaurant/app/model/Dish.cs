namespace Restaurant.app.model;

public class Dish
{
    public int DishId { get; set; }
    public int GroupId { get; set; }
    public string DishName { get; set; }
    public decimal DishCost { get; set; }
    public float OutputWeight { get; set; }
    public string CookingTechnology { get; set; }
    public byte[] Photo { get; set; }
}
