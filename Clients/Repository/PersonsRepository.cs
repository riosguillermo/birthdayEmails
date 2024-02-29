using Azure.Core;
using Azure.Identity;
using cumples.DataModel.Dtos.Gender;
using cumples.DataModel.Dtos.Persons;
using cumples.DataModel.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace cumples.Infrastructure.Repository
{
    public class PersonsRepository
    {
        #region Constructor
        private readonly CumplesContext _dbContext;

        public PersonsRepository(
            CumplesContext cumplesContext
            )
        {
            _dbContext = cumplesContext;
        }
        #endregion Constructor

        public Person? GetPersonById(int id)
        {
            return _dbContext.Persons.FirstOrDefault(p => p.PersonsId == id);
        }

        public List<Person> GetAllPersons()
        {
            return _dbContext.Persons.ToList();
        }

        public List<Person> GetAllActivePersons()
        {
            return _dbContext.Persons.Where(p => p.Active == true).ToList();
        }

        public CreatePersonResponseDto MapPersonToCreatePersonResponseDto(Person person)
        {
            return new CreatePersonResponseDto
            {
                PersonsId = person.PersonsId,
                GenderId = person.GenderId,
                LastName = person.LastName,
                FirstName = person.FirstName,
                MiddleName = person.MiddleName,
                Birthday = person.Birthday,
                Email = person.Email,
                Address = person.Address,
                Active = person.Active,
            };
        }

        public GetPersonResponseDto MapPersonToGetPersonResponseDto(Person person)
        {
            return new GetPersonResponseDto
            {
                PersonsId = person.PersonsId,
                GenderId = person.GenderId,
                LastName = person.LastName,
                FirstName = person.FirstName,
                MiddleName = person.MiddleName,
                Birthday = person.Birthday,
                Email = person.Email,
                Address = person.Address,
                Active = person.Active,
            };
        }

        public List<GetPersonResponseDto> mapPersonsListToPersonResponseDtoList(List<Person> persons)
        { 
            List<GetPersonResponseDto> list = new List<GetPersonResponseDto>();
            foreach (Person person in persons)
            {
                list.Add(MapPersonToGetPersonResponseDto(person));
            }
            return list;
        }

        public string GetGenderDescriptionByGenderId(int genderId)
        {
            Gender? gender = _dbContext.Genders.FirstOrDefault(g => g.GenderId == genderId);
            if (gender != null && gender.Active == false) gender = null;
            return gender != null ? gender.Description : "Gender not found";
        }

        public Person CreatePerson(CreatePersonRequestDto newPerson)
        {
            Person person = new Person()
            {
                GenderId = newPerson.GenderId,
                LastName = newPerson.LastName,
                FirstName = newPerson.FirstName,
                MiddleName = newPerson.MiddleName,
                Birthday = newPerson.Birthday,
                Email = newPerson.Email,
                Address = newPerson.Address,
                CreatedDate = DateTime.Now,
                CreatedBy = "test",
                ModifiedDate = DateTime.Now,
                ModifiedBy = "test"
            };

            _dbContext.Persons.Add(person);
            _dbContext.SaveChanges();


            return person;

        }

        public PutPersonResponseDto PutPerson(Person person, PutPersonRequestDto newPerson)
        {
            PutPersonResponseDto response = new PutPersonResponseDto()
            {
                PersonsId = person.PersonsId,
                GenderId = person.GenderId,
                Gender = GetGenderDescriptionByGenderId(person.GenderId),
                LastName = person.LastName,
                FirstName = person.FirstName,
                MiddleName = person.MiddleName,
                Birthday = person.Birthday.ToShortDateString(),
                Email = person.Email,
                Address = person.Address,
                Active = person.Active.ToString()
            };

            if(newPerson.NewGenderId > 0 && person.GenderId != newPerson.NewGenderId)
            {
                response.Gender = GetGenderDescriptionByGenderId(person.GenderId) + " --> " + GetGenderDescriptionByGenderId((int)newPerson.NewGenderId);
                person.GenderId = (int)newPerson.NewGenderId;
            }

            if(newPerson.NewLastName != null && person.LastName != newPerson.NewLastName)
            {
                response.LastName = person.LastName + " --> " + newPerson.NewLastName;
                person.LastName = newPerson.NewLastName;
            }

            if (newPerson.NewFirstName != null && person.FirstName != newPerson.NewFirstName)
            {
                response.FirstName = person.FirstName + " --> " + newPerson.NewFirstName;
                person.FirstName = newPerson.NewFirstName;
            }

            if(newPerson.MiddleNameIsNull == true && person.MiddleName != null)
            {
                response.MiddleName = person.MiddleName + " --> " + "Null";
                person.MiddleName = null;
            }
            else if (newPerson.NewMiddleName != null && person.MiddleName != newPerson.NewMiddleName && newPerson.MiddleNameIsNull != true)
            {
                response.MiddleName = person.MiddleName + " --> " + newPerson.NewMiddleName;
                person.MiddleName = newPerson.NewMiddleName;
            }

            if(newPerson.NewBirthday != null && newPerson.NewBirthday != DateTime.MinValue && person.Birthday != newPerson.NewBirthday)
            {
                response.Birthday = person.Birthday.ToShortDateString() + " --> " + newPerson.NewBirthday.ToString();
                person.Birthday = (DateTime)newPerson.NewBirthday;
            }

            if(newPerson.emailIsNull == true && person.Email != null)
            {
                response.Email = person.Email + " --> " + "Null";
                person.Email = null;
            }
            else if(newPerson.NewEmail != null && person.Email !=  newPerson.NewEmail && newPerson.emailIsNull != true)
            {
                response.Email = person.Email + " --> " + newPerson.NewEmail;
                person.Email = newPerson.NewEmail;
            }

            if(newPerson.AddressIsNull == true && person.Address != null)
            {
                response.Address = person.Address + " --> " + "Null";
                person.Address = null;
            }
            else if(newPerson.NewAddress != null && person.Address != newPerson.NewAddress && newPerson.AddressIsNull != true)
            {
                response.Address = person.Address + " --> " + newPerson.NewAddress;
                person.Address = newPerson.NewAddress;
            }

            if(newPerson.NewActive != null &&  person.Active != newPerson.NewActive)
            {
                response.Active = person.Active.ToString() + " --> " + newPerson.NewActive.ToString();
                person.Active = newPerson.NewActive;
            }

            person.ModifiedDate = DateTime.Now;
            _dbContext.SaveChanges();
            return response;
        }
    }
}
