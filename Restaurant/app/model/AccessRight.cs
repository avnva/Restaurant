using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant;

public class AccessRight
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int ID { get; set; }

    public bool Read { get; set; }

    public bool Write { get; set; }

    public bool Edit { get; set; }

    public bool Delete { get; set; }

    //public FormTypes Form { get; set; }

}
