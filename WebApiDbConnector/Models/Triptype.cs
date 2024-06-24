using System;
using System.Collections.Generic;

namespace WebApiDbConnector.Models;

public partial class Triptype
{
    public int Id { get; set; }

    public string? Typecontract { get; set; }

    public virtual ICollection<Trip> Trips { get; set; } = new List<Trip>();
}
