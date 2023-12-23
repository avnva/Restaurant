using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Restaurant.app.model;

public class RequestsProducts
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Key]
    [Column(Order = 1)]
    public int RequestID { get; set; }
    [Key]
    [Column(Order = 2)]
    public int ProductID { get; set; }
    public double Quantity { get; set; }

    [ForeignKey(nameof(RequestID))]
    public Request Request { get; set; }

    [ForeignKey(nameof(ProductID))]
    public Product Product { get; set; }
}
