using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant;

public class Warehouse
{
    public int WarehouseId { get; set; }
    public int ProductId { get; set; }
    public float StockBalance { get; set; }
    public int SupplierId { get; set; }
}
