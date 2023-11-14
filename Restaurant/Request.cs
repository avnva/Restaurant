using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant;

public class Request
{
    public int RequestID { get; set; }
    public int DepartmentID { get; set; }
    public int RequestTypeID { get; set; }
    public DateTime RequestDate { get; set; }
}
