using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cumples.DataModel.Dtos.Log
{
    public partial class CreateLogRequestDto
    {
        public DateTime LogDate { get; set; }

        public string LogType { get; set; } = null!;

        public string LogProcess { get; set; } = null!;

        public string LogEntity { get; set; } = null!;

        public int LogEntityId { get; set; }

        public string? LogMessage { get; set; }
    }
}
