using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cumples.DataModel.Dtos.Persons
{
    public partial class PutPersonResponseDto
    {
        public int PersonsId { get; set; }

        public int GenderId { get; set; }

        public string? Gender { get; set; }

        public string LastName { get; set; } = null!;

        public string? FirstName { get; set; }

        public string? MiddleName { get; set; }

        public string? Birthday { get; set; }

        public string? Email { get; set; }

        public string? Address { get; set; }

        public string? Active { get; set; }

    }
}
