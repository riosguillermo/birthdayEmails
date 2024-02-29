﻿using System;
using System.Collections.Generic;

namespace cumples.DataModel.Dtos.Gender;

public partial class GetGenderResponseDto
{
    public int GenderId { get; set; }

    public string Description { get; set; } = null!;

    public string? Prefix { get; set; }

    public bool? Active { get; set; }
}
