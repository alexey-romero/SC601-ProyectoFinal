using RepositoryLayer.Models;
using System;
using System.Collections.Generic;

namespace RepositoryLayer.Models;

public partial class RequestStatus
{
    public int Id { get; set; }

    public string Status { get; set; } = null!;

    public virtual ICollection<Request> Requests { get; set; } = new List<Request>();
}
