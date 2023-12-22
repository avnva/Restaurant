using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Restaurant.app.model;

public class SuppliesProducts
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Key]
    [Column(Order = 1)]
    public int SupplyID { get; set; }
    [Key]
    [Column(Order = 2)]
    public int ProductID { get; set; }
    public double DeliveredQuantity { get; set; }

    [ForeignKey(nameof(SupplyID))]
    public Supply Supply { get; set; }

    [ForeignKey(nameof(ProductID))]
    public Product Product { get; set; }
}
