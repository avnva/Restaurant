using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant;

public class Product
{
    public int ProductId { get; set; }
    public string ProductName { get; set; }
    public int UnitsOfMeasureId { get; set; }
    public decimal PriceMarkup { get; set; }
}