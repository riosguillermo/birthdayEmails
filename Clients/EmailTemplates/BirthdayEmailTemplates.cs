using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cumples.Infrastructure.EmailTemplates
{
    public class BirthdayEmailTemplates
    {
        public string GetBirthdayPersonTemplate(string name)
        {
            string template = @"
        <!DOCTYPE html>
        <html lang=""en"">
        <head>
            <meta charset=""UTF-8"">
            <meta name=""viewport"" content=""width=device-width, initial-scale=1.0"">
            <title>¡Feliz Cumpleaños!</title>
        </head>
        <body>
            <div style=""font-family: Arial, sans-serif; max-width: 600px; margin: 0 auto; padding: 20px;"">
                <h4 style=""color: #007bff;"">¡Feliz cumpleaños [Nombre]!</h4>

                En este día especial, queremos desearte un día lleno de alegría, amor y felicidad.<br>
                <br>
                Apreciamos tenerte en nuestra comunidad y esperamos que disfrutes de tu día al máximo.<br>
                <br>
                ¡Que tengas un año maravilloso por delante!</p>

                <p>Saludos cordiales,</p>
                <p>Irsa</p>
            </div>
        </body>
        </html>
        ";

            template = template.Replace("[Nombre]", name);

            return template;
        }

        public string GetNonBirthdayPersonTemplate(List<string> BirthdayPersonNames)
        {
            string template = @"
        <!DOCTYPE html>
        <html lang=""en"">
        <head>
            <meta charset=""UTF-8"">
            <meta name=""viewport"" content=""width=device-width, initial-scale=1.0"">
            <title>Cumpleaños de hoy</title>
        </head>
        <body>
            <div style=""font-family: Arial, sans-serif; max-width: 600px; margin: 0 auto; padding: 20px;"">
                 
                <p>Te escribimos para recordarte que hoy es el cumpleaños de:</p>
                    
                 <p style=""color: #007bff;"">[Nombres]</p>";
                

            if(BirthdayPersonNames.Count > 1)
            {
                template = template.Replace("[Nombres]", string.Join("<br>", BirthdayPersonNames));
                template += @"<p>¡Envíales tus mejores deseos para que tengan un día increíble!</p>";
            }
            else
            {
                template = template.Replace("[Nombres]", BirthdayPersonNames[0]);
                template += @"<p>¡Envíale tus mejores deseos para que tenga un día increíble!</p>";
                
            }

            template += @" <p>Saludos cordiales,</p>
                            <p>Irsa</p>
                            </div>
                            </body>
                         </html>";

            return template;
        }

    }
}
