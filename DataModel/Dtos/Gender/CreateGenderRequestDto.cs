using System;
using System.Collections.Generic;

namespace cumples.DataModel.Dtos.Gender;

public partial class CreateGenderRequestDto
{
    public string Description { get; set; } = null!;

    public string? Prefix { get; set; }

    public bool? Active { get; set; }
}
