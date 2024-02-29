using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cumples.Services.Interfaces
{
    public interface IEmailService
    {
        Task<bool> Send(string body, string addresses, string cc, string cco, string subject, int idApp, DateTime? programadoPara = null, string from = "no-responder@irsa.com.ar");
    }
}
