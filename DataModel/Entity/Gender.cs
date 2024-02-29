using System;
using System.Collections.Generic;

namespace cumples.DataModel.Entity;

public partial class Gender
{
    public int GenderId { get; set; }

    public string Description { get; set; } = null!;

    public string? Prefix { get; set; }

    public DateTime CreatedDate { get; set; }

    public string CreatedBy { get; set; } = null!;

    public DateTime ModifiedDate { get; set; }

    public string ModifiedBy { get; set; } = null!;

    public bool? Active { get; set; }

    public virtual ICollection<Person> People { get; set; } = new List<Person>();
}
