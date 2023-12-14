using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant;

public class RequestType
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Key]
    public int RequestTypeId { get; set; }
    public string RequestTypesName { get; set; }
}
