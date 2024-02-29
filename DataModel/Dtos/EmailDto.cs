using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cumples.DataModel.Dtos
{
    public class EmailDto
    {
        public DateTime? ProgramadoPara { get;  set; }
        public bool EsHtml { get;  set; }
        public string Destinatario { get;  set; } = default!;
        public string Remitente { get;  set; } = default!;
        public string Cuerpo { get;  set; } = default!;
        public string Cco { get;  set; } = default!;
        public string Cc { get; set; } = default!;
        public string Asunto { get; set; } = default!;
        public int IdSistema { get; set; }
        public List<AdjuntoDto>? Adjuntos { get; set; }
    }

    public class AdjuntoDto
    {
        public string Nombre { get; set; } = default!;
        public bool Embebido { get; set; }
    }
}
