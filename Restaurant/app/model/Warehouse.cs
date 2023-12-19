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
    public int WarehouseID { get; set; }
    public int ProductID { get; set; }
    public double StockBalance { get; set; }
    public int SupplierID { get; set; }

    [ForeignKey("ProductID")]
    public Product Product { get; set; }
    [ForeignKey("SupplierID")]
    public Supplier Supplier { get; set; }
}
