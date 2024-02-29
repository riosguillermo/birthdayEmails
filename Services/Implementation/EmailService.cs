using cumples.DataModel.Dtos;
using cumples.DataModel.Dtos.Persons;
using cumples.Services.Interfaces;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace cumples.Services.Implementation
{
    public class EmailService : IEmailService
    {

        public static IConfiguration _config { get; set; }

        public EmailService(IConfiguration config)
        {
            _config = config;
        }

        public async Task<bool> Send(string body, string addresses, string cc, string cco, string subject, int idApp, DateTime? programadoPara = null, string from = "no-responder@irsa.com.ar")
        {
            var adjuntos_mail = new List<AdjuntoDto>();
            //if (adjuntos != null)
            //{
            //    adjuntos_mail = adjuntos.Select(a => new AdjuntoDto()
            //    {
            //        Embebido = a.Embebido,
            //        Nombre = a.Nombre
            //    }).ToList();
            //}
            var mailDto = new EmailDto()
            {
                Adjuntos = adjuntos_mail,
                Asunto = HttpUtility.HtmlEncode(subject),
                Cc = string.IsNullOrEmpty(cc) ? "" : cc,
                Cco = string.IsNullOrEmpty(cco) ? "" : cco,
                Cuerpo = HttpUtility.HtmlEncode(body),
                Destinatario = addresses,
                EsHtml = true,
                ProgramadoPara = programadoPara,
                Remitente = from,
                IdSistema = idApp
            };

            var mailJson = JsonConvert.SerializeObject(mailDto);

            HttpContent stringContent = new StringContent(mailJson, Encoding.UTF8, "application/json");

            using (var client = new HttpClient())
            using (var formData = new MultipartFormDataContent())
            {
                client.DefaultRequestHeaders.TransferEncodingChunked = true;
                formData.Add(stringContent, "emailDto");
                //if (adjuntos != null)
                //{
                //    foreach (var adjunto in adjuntos)
                //    {
                //        formData.Add(new StreamContent(adjunto.Stream()), "file", adjunto.Nombre);
                //    }
                //}

                var response = await client.PostAsync($"{_config["Email:Endpoint"]}", formData);
                var responseStr = await response.Content.ReadAsStringAsync();
                if (!response.IsSuccessStatusCode)
                {
                    return false;
                }

                return true;
            }
        }
    }
}
