using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant;

public class Supply
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Key]
    public int SupplyID { get; set; }
    public int SupplierID { get; set; }
    public DateTime SupplyDate { get; set; }
    public decimal PurchasePrice { get; set; }

    [ForeignKey("SupplierID")]
    public Supplier Supplier { get; set; }
}
