using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Restaurant;

public class User
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Key]
    [Column("UserID")]
    public int UserId { get; set; }

    [MinLength(5)]
    [Column("Username")]
    public string Login { get; set; }

    [MinLength(5)]
    public string PasswordHash { get; set; }
    public int RoleID { get; set; }

    //public UserRole UserRole { get; set; }

    //public List<AccessRight> AccessRights { get; set; }
}