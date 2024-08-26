using System;
using System.Collections.Generic;

namespace RepositoryLayer.Models;

public partial class Request
{
    public int Id { get; set; }

    public int RequestType { get; set; }

    public int RequestStatus { get; set; }

    public string Title { get; set; } = null!;

    public string? Description { get; set; }

    public DateOnly? EstimatedDueDate { get; set; }

   // public DateTime CreationDate { get; set; }

    public DateOnly? RevokePermissionDate { get; set; }

    public string? AdminNotes { get; set; }

    public string? ResolutionInfo { get; set; }

    public int IdUser { get; set; }

    public int? IdManager { get; set; }

    public int? IdAdmin { get; set; }

    public virtual User? IdAdminNavigation { get; set; }

    public virtual User? IdManagerNavigation { get; set; }

    public virtual User IdUserNavigation { get; set; } = null!;

    public virtual RequestStatus RequestStatusNavigation { get; set; } = null!;

    public virtual RequestType RequestTypeNavigation { get; set; } = null!;
}
