using cumples.DataModel.Dtos;
using cumples.DataModel.Dtos.Persons;
using cumples.DataModel.Entity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace cumples.Infrastructure.Repository
{
    public class BirthdayRepository
    {
        #region Constructor
        private readonly CumplesContext _dbContext;



        public BirthdayRepository(CumplesContext cumplesContext)
        {
            _dbContext = cumplesContext;
        }
        #endregion Constructor

        public List<Person> GetTodayBirthdayPerson()
        {
            DateTime today = DateTime.Today;
            return _dbContext.Persons.Where(p => p.Birthday.Month == today.Month && p.Birthday.Day == today.Day && p.Active == true).ToList();
        }

        public List<Person> GetTodayNonBirthdayPerson()
        {
            DateTime today = DateTime.Today;
            return _dbContext.Persons.Where(p => !(p.Birthday.Month == today.Month && p.Birthday.Day == today.Day && p.Active == true)).ToList();
        }

        public string GetEmailCcoStringFromPersonList(List<Person> people)
        {
            List<string?> emails = people.Select(p => p.Email).ToList();
            emails = FilterInvalidEmails(emails);
            return string.Join(";", emails);
        }

        public async Task<List<MailingPerson>> GetMailingPersons()

        {
            var list = await _dbContext.Persons
                .Where(x => x.Active == true)
                .Select(x => new { x.FirstName, x.LastName, x.GenderId, x.Email, x.Birthday })
                .ToListAsync();

            DateTime today = DateTime.Today;

            var response = new List<MailingPerson>();

            if (list.Where(p => p.Birthday.Month == today.Month && p.Birthday.Day == today.Day).Count() > 0)
            {
                foreach(var person in list)
                {
                    
                    var mailingperson_line = new MailingPerson();
                    
                    if (string.IsNullOrEmpty(person.Email) || !IsValidEmail(person.Email))
                    {
                        mailingperson_line.isEmailValid = false;
                    }

                    if (person.Birthday.Month == today.Month && person.Birthday.Day == today.Day)
                    {
                        mailingperson_line.isTodayBirthday = true;
                    }

                    mailingperson_line.Birthday = person.Birthday;
                    mailingperson_line.Name = person.FirstName + ' ' + person.LastName;
                    mailingperson_line.GenderId = person.GenderId;
                    mailingperson_line.Email = mailingperson_line.isEmailValid ? person.Email : null;

                    response.Add(mailingperson_line);
                }
            }

            return response;
        }

        public List<string?> FilterInvalidEmails(List<string?> emails)
        {
            return emails.Where(email => email != null && IsValidEmail(email)).ToList();
        }


        public bool IsValidEmail(string email)
        {
            var trimmedEmail = email.Trim();

            if (trimmedEmail.EndsWith("."))
            {
                return false;
            }
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == trimmedEmail;
            }
            catch
            {
                return false;
            }
        }

    }
}
