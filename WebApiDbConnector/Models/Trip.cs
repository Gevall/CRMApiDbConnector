using System;
using System.Collections.Generic;

namespace WebApiDbConnector.Models;

public partial class Trip
{
    public int Id { get; set; }

    public int? ManagerId { get; set; }

    public int? EmployeId { get; set; }

    public DateTime? TripDate { get; set; }

    public DateTime? ContractDate { get; set; }

    public DateTime? DeadlineContract { get; set; }

    public int? CompanyId { get; set; }

    public string? Customer { get; set; }

    public string? CustomerAddress { get; set; }

    public int? TripTypeId { get; set; }

    public string? Caption { get; set; }

    public virtual Company? Company { get; set; }

    public virtual Employe? Employe { get; set; }

    public virtual Manager? Manager { get; set; }

    public virtual Triptype? TripType { get; set; }
}
