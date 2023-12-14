using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant;

public class Product
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Key]
    public int ProductId { get; set; }
    public string ProductName { get; set; }
    public int UnitsOfMeasureId { get; set; }
    public decimal PriceMarkup { get; set; }
}