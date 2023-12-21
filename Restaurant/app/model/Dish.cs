using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Net.Sockets;
using System.Windows.Documents;

namespace Restaurant.app.model;

public class Dish
{

    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int? DishID { get; set; }

    [Required(ErrorMessage = "Необходимо выбрать категорию")]
    public int GroupID { get; set; }

    [Required(ErrorMessage = "Введите название блюда")]
    public string DishName { get; set; }

    [Required(ErrorMessage = "Укажите стоимость блюда")]
    public decimal DishCost { get; set; }

    [Required(ErrorMessage = "Введите вес порции")]
    public double OutputWeight { get; set; }

    [Required(ErrorMessage = "Добавьте технологию приготовления")]
    public string CookingTechnology { get; set; }

    public string? Photo { get; set; }

    [NotMapped]
    public DishGroup DishGroup { get; set; }

    [NotMapped]
    public Status Status { get; set; }
}
