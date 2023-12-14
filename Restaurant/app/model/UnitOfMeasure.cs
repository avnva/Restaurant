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
    public int UnitOfMeasureId { get; set; }
    public string UnitOfMeasureName { get; set; }
}
