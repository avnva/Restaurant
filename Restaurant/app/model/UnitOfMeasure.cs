using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant;

public class UnitOfMeasure
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Key]
    public int UnitsOfMeasureID { get; set; }
    public string UnitsOfMeasureName { get; set; }
}
