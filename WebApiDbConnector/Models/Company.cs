using System;
using System.Collections.Generic;

namespace WebApiDbConnector.Models;

public partial class Company
{
    public int Id { get; set; }

    public string? CompanyName { get; set; }

    public virtual ICollection<Trip> Trips { get; set; } = new List<Trip>();
}
