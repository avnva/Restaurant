using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Restaurant;

public class User
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int UserID { get; set; }

    [MinLength(5)]
    public string Login { get; set; }

    [MinLength(5)]
    public string PasswordHash { get; set; }

    //public UserRoles UserRole { get; set; }

    //public List<AccessRight> AccessRights { get; set; }
}