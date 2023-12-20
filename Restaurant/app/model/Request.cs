using Restaurant.app.model;
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
    public int RequestID { get; set; }
    public int DepartmentID { get; set; }
    public DateTime RequestDate { get; set; }
    [ForeignKey("DepartmentID")]
    public Department Department { get; set; }
}
