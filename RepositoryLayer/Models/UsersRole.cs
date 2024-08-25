using System;
using System.Collections.Generic;

namespace TicketingSystem.Models;

public partial class UsersRole
{
    public int IdUsersRoles { get; set; }

    public int IdUser { get; set; }

    public string IdRole { get; set; } = null!;

    public virtual UserRole IdUser1 { get; set; } = null!;

    public virtual User IdUserNavigation { get; set; } = null!;
}
