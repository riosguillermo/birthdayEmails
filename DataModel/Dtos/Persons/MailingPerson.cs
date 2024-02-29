using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cumples.DataModel.Dtos.Persons
{
    public class MailingPerson
    {
        public string Name { get; set; } = null!;
        public DateTime Birthday { get; set; }
        public string? Email { get; set; }
        public int GenderId { get; set; }
        public bool isTodayBirthday { get; set; } = false;
        public bool isEmailValid { get; set; } = true;
    }
}
