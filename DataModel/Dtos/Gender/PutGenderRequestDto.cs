using System;
using System.Collections.Generic;

namespace cumples.DataModel.Dtos.Gender;

public partial class PutGenderRequestDto
{
    public int GenderId { get; set; }

    public string? NewDescription { get; set; }

    public string? NewPrefix { get; set; }

    public bool? NewActive { get; set; }

}
