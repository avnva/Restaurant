using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant;

public class Request
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Key]
    public int RequestId { get; set; }
    public int DepartmentId { get; set; }
    public int RequestTypeId { get; set; }
    public DateTime RequestDate { get; set; }
}
