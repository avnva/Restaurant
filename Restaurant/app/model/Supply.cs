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
    public int SupplyId { get; set; }
    public int SupplierId { get; set; }
    public DateTime SupplyDate { get; set; }
    public decimal PurchasePrice { get; set; }
}
