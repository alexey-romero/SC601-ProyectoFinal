using System;
using System.Collections.Generic;

namespace TicketingSystem.Models;

public partial class User
{
    public int Id { get; set; }

    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string Password { get; set; } = null!;

    public int? Phone { get; set; }

    public string? JobTitle { get; set; }

    public int? IdManager { get; set; }

    public virtual ICollection<Request> RequestIdAdminNavigations { get; set; } = new List<Request>();

    public virtual ICollection<Request> RequestIdManagerNavigations { get; set; } = new List<Request>();

    public virtual ICollection<Request> RequestIdUserNavigations { get; set; } = new List<Request>();

    public virtual ICollection<UsersRole> UsersRoles { get; set; } = new List<UsersRole>();
}
