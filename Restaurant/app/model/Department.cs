using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.app.model;

public class Department
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Key]
    [Column("DepartmentID")]
    public int DepartmentID { get; set; }
    public string DepartmentName { get; set; }
    public string ActionsDescription { get; set; }
}
