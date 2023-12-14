using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant;

public class Warehouse
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Key]
    public int WarehouseId { get; set; }
    public int ProductId { get; set; }
    public float StockBalance { get; set; }
    public int SupplierId { get; set; }
}
