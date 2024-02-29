using cumples.DataModel.Entity;
using cumples.Services.Interfaces;
using cumples.Infrastructure.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using cumples.DataModel.Dtos.Persons;
using cumples.Infrastructure.EmailTemplates;

namespace cumples.Services.Implementation
{
    public class BirthdayServices : IBirthdayServices
    {
        private readonly CumplesContext _dbContext;
        private IEmailService _emailService;
        private BirthdayRepository _repository;
        private BirthdayEmailTemplates _birthdayEmailTemplates;

        public BirthdayServices(IEmailService emailService, CumplesContext cumplesContext)
        {
            _emailService = emailService;
            _dbContext = cumplesContext;
            _repository = new BirthdayRepository(_dbContext);
            _birthdayEmailTemplates = new BirthdayEmailTemplates();
        }

        public async Task<ResponseDto<string>> SendTodayBirthdayEmails()
        {
            string response = "No se enviaron mails. No hay cumpleañeros hoy";

            List<MailingPerson> involvedPeople = await _repository.GetMailingPersons();

            if (involvedPeople.Count > 0)
            {
                foreach (var birthdayPerson in involvedPeople.Where(x =>x.isTodayBirthday && x.isEmailValid))
                {
                    await _emailService.Send(
                        _birthdayEmailTemplates.GetBirthdayPersonTemplate(birthdayPerson.Name),//"Prueba Cumpleañeros - Cuerpo Email", //Cuando armas el Body trabajas con birthdayperson.Name
                        birthdayPerson.Email,
                        "",
                        "",
                        "¡Sorpresa!",
                        0
                   );
                }

                await _emailService.Send(
                    _birthdayEmailTemplates.GetNonBirthdayPersonTemplate(involvedPeople.Where(x => x.isTodayBirthday).Select(x => x.Name).ToList()), //Cuando armas el Body trabajas con string.Join(" - ", involucrados.Where(x => x.isTodayBirthday).Select(x => new { x.Name }).ToList()); 
                    "",
                    "",
                    string.Join(";", involvedPeople.Where(x => !x.isTodayBirthday && x.isEmailValid).Select(x => x.Email).ToList()),
                    "Cumpleañeros de hoy",
                    0
                );

                response = "Emails de cumpleaños enviados.";


            }

            return new ResponseDto<string>()
            {
                Status = HttpStatusCode.OK,
                Message = "Envio de mails de cumpleaños",
                Data = response
            };
        }
    }
}
