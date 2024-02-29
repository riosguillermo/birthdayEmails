using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cumples.DataModel.Dtos.Log
{
    public partial class PutLogResponseDto
    {
        public int LogId { get; set; }

        public string LogDate { get; set; } = null!;

        public string User { get; set; } = null!;

        public string LogType { get; set; } = null!;

        public string LogProcess { get; set; } = null!;

        public string LogEntity { get; set; } = null!;

        public string LogEntityId { get; set; }

        public string? LogMessage { get; set; }
    }
}
