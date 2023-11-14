using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant;

public class Supply
{
    public int SupplyID { get; set; }
    public int SupplierID { get; set; }
    public DateTime SupplyDate { get; set; }
    public decimal PurchasePrice { get; set; }
}
