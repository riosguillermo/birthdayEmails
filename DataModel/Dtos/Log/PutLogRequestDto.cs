using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cumples.DataModel.Dtos.Log
{
    public partial class PutLogRequestDto
    {
        public int LogId { get; set; }

        public DateTime? NewLogDate { get; set; }

        public string? NewLogType { get; set; }

        public string? NewLogProcess { get; set; }

        public string? NewLogEntity { get; set; }

        public int? NewLogEntityId { get; set; }

        public string? NewLogMessage { get; set; }
    }
}
