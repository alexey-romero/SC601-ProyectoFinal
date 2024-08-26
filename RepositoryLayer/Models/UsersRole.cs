using System;
using System.Collections.Generic;

namespace RepositoryLayer.Models;

public partial class UsersRole
{
    public int IdUsersRoles { get; set; }

    public int IdUser { get; set; } // This should be an int

    public int IdRole { get; set; } // This should be an int to match the database schema

    public virtual User IdUserNavigation { get; set; } = null!;

    public virtual UserRole IdRoleNavigation { get; set; } = null!;
}

