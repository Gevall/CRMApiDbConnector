using System;
using System.Collections.Generic;

namespace WebApiDbConnector.Models;

public partial class Employe
{
    public int Id { get; set; }

    public string? Firstname { get; set; }

    public string? Lastname { get; set; }

    public string? Patronymic { get; set; }

    public virtual ICollection<Trip> Trips { get; set; } = new List<Trip>();
}
