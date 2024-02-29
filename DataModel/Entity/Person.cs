using System;
using System.Collections.Generic;

namespace cumples.DataModel.Entity;

public partial class Person
{
    public int PersonsId { get; set; }

    public int GenderId { get; set; }

    public string LastName { get; set; } = null!;

    public string FirstName { get; set; } = null!;

    public string? MiddleName { get; set; }

    public DateTime Birthday { get; set; }

    public string? Email { get; set; }

    public string? Address { get; set; }

    public DateTime CreatedDate { get; set; }

    public string CreatedBy { get; set; } = null!;

    public DateTime ModifiedDate { get; set; }

    public string ModifiedBy { get; set; } = null!;

    public bool? Active { get; set; }

    public virtual Gender Gender { get; set; } = null!;
}
