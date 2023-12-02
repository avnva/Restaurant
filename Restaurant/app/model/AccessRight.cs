using System.ComponentModel.DataAnnotations.Schema;

namespace Restaurant.app.model;

public class AccessRight
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    public bool Read { get; set; }

    public bool Write { get; set; }

    public bool Edit { get; set; }

    public bool Delete { get; set; }
}
