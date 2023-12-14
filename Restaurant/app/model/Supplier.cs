using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant;

public class Supplier
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Key]
    public int SupplierId { get; set; }
    public string SupplierName { get; set; }
    public string Address { get; set; }
    public string ContactPersonName { get; set; }
    public string Phone { get; set; }
    public string BankName { get; set; }
    public string BankAccount { get; set; }
    public string INN { get; set; }
}
