using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cumples.DataModel.Dtos.Persons
{
    public partial class PutPersonRequestDto
    {
        public int PersonsId { get; set; }

        public int? NewGenderId { get; set; }

        public string? NewLastName { get; set; } = null!;

        public string? NewFirstName { get; set; } = null!;

        public string? NewMiddleName { get; set; }

        public bool MiddleNameIsNull { get; set; }

        public DateTime? NewBirthday { get; set; }

        public string? NewEmail { get; set; }

        public bool emailIsNull { get; set; }

        public string? NewAddress { get; set; }
        
        public bool AddressIsNull { get; set; }

        public bool? NewActive { get; set; }

    }
}
