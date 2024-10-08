﻿using System;
using System.Collections.Generic;

namespace RepositoryLayer.Models;

public partial class RequestType
{
    public int Id { get; set; }

    public string Type { get; set; } = null!;

    public virtual ICollection<Request> Requests { get; set; } = new List<Request>();
}
