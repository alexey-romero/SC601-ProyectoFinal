using System;
using System.Collections.Generic;

namespace TicketingSystem.Models;

public partial class UserRole
{
    public int Id { get; set; }

    public string RoleName { get; set; } = null!;

    public virtual ICollection<UsersRole> UsersRoles { get; set; } = new List<UsersRole>();
}
