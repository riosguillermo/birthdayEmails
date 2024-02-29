using Azure;
using cumples.DataModel.Dtos.Gender;
using cumples.DataModel.Dtos.Log;
using cumples.DataModel.Dtos.Persons;
using cumples.DataModel.Entity;
using cumples.Infrastructure.Repository;
using cumples.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace cumples.Services.Implementation
{
    public class PersonsServices : IPersonsServices
    {
        #region Constructor
        private readonly CumplesContext _dbContext;
        private readonly PersonsRepository _repository;
        private ILogServices _logServices;
        public PersonsServices(CumplesContext cumplesContext, ILogServices logServices)
        {
            _dbContext = cumplesContext;
            _repository = new PersonsRepository(_dbContext);
            _logServices = logServices;
        }
        #endregion Constructor


        public async Task<ResponseDto<CreatePersonResponseDto>> CreatePerson(CreatePersonRequestDto request)
        {
            #region Validaciones
            if (string.IsNullOrEmpty(request.LastName))
            {
                return new ResponseDto<CreatePersonResponseDto>()
                {
                    Status = HttpStatusCode.BadRequest,
                    Message = "El campo Apellido es obligatorio",
                    Data = null
                };
            }
            if (request.LastName.Length > 50)
            {
                return new ResponseDto<CreatePersonResponseDto>()
                {
                    Status = HttpStatusCode.BadRequest,
                    Message = "El Apellido excede los 50 caracteres",
                    Data = null
                };
            }

            if (string.IsNullOrEmpty(request.FirstName))
            {
                return new ResponseDto<CreatePersonResponseDto>()
                {
                    Status = HttpStatusCode.BadRequest,
                    Message = "El campo Nombre es obligatorio",
                    Data = null
                };
            }

            if (request.FirstName.Length > 50)
            {
                return new ResponseDto<CreatePersonResponseDto>()
                {
                    Status = HttpStatusCode.BadRequest,
                    Message = "El Nombre excede los 50 caracteres",
                    Data = null
                };
            }

            if (request.MiddleName != null && request.MiddleName.Length > 50)
            {
                return new ResponseDto<CreatePersonResponseDto>()
                {
                    Status = HttpStatusCode.BadRequest,
                    Message = "El Segundo Nombre excede los 50 caracteres",
                    Data = null
                };
            }

            if (request.Email != null && request.Email.Length > 50)
            {
                return new ResponseDto<CreatePersonResponseDto>()
                {
                    Status = HttpStatusCode.BadRequest,
                    Message = "El Email excede los 50 caracteres",
                    Data = null
                };
            }

            if (request.Address != null && request.Address.Length > 50)
            {
                return new ResponseDto<CreatePersonResponseDto>()
                {
                    Status = HttpStatusCode.BadRequest,
                    Message = "La dirección excede los 50 caracteres",
                    Data = null
                };
            }

            if (request.Birthday == DateTime.MinValue)
            {
                return new ResponseDto<CreatePersonResponseDto>()
                {
                    Status = HttpStatusCode.BadRequest,
                    Message = "La fecha de nacimiento debe ser valida",
                    Data = null
                };
            }

            if (_dbContext.Genders.FirstOrDefault(g => g.GenderId == request.GenderId) == null)
            {
                return new ResponseDto<CreatePersonResponseDto>()
                {
                    Status = HttpStatusCode.BadRequest,
                    Message = "El IdGenero debe ser valido",
                    Data = null
                };
            }

            #endregion

            request.FirstName = request.FirstName.ToUpper();
            request.LastName = request.LastName.ToUpper();
            if (request.MiddleName != null) request.MiddleName = request.MiddleName.ToUpper();
            if (request.Active == null) request.Active = true;

            CreatePersonResponseDto response = _repository.MapPersonToCreatePersonResponseDto(_repository.CreatePerson(request));

            await _logServices.CreateLog(new CreateLogRequestDto()
            {
                LogDate = DateTime.Now,
                LogType = "PERSON",
                LogProcess = "CREATE",
                LogEntity = response.LastName,
                LogEntityId = response.PersonsId,
                LogMessage = ""
            });

            return new ResponseDto<CreatePersonResponseDto>
            {
                Status = HttpStatusCode.OK,
                Message = "Person successfully created",
                Data = response
            };
        }

        public async Task<ResponseDto<GetPersonResponseDto>> GetPersonById(int id)
        {
            Person? person = _repository.GetPersonById(id);

            if(person != null )
            {
                return new ResponseDto<GetPersonResponseDto>()
                {
                    Status = HttpStatusCode.OK,
                    Message = "Person Found",
                    Data = _repository.MapPersonToGetPersonResponseDto(person)
                };
            }
            else
            {
                return new ResponseDto<GetPersonResponseDto>()
                {
                    Status = HttpStatusCode.NotFound,
                    Message = "Person not found",
                    Data = null
                };
            }
        }

        public async Task<ResponseDto<PutPersonResponseDto>> PutPerson(PutPersonRequestDto request)
        {
            Person? person = _repository.GetPersonById(request.PersonsId);
            if (person != null)
            {
                #region Validaciones

                if (request.NewLastName != null && request.NewLastName.Length > 50)
                {
                    return new ResponseDto<PutPersonResponseDto>()
                    {
                        Status = HttpStatusCode.BadRequest,
                        Message = "El Apellido excede los 50 caracteres",
                        Data = null
                    };
                }

                if (request.NewFirstName != null && request.NewFirstName.Length > 50)
                {
                    return new ResponseDto<PutPersonResponseDto>()
                    {
                        Status = HttpStatusCode.BadRequest,
                        Message = "El Nombre excede los 50 caracteres",
                        Data = null
                    };
                }

                if (request.NewMiddleName != null && request.NewMiddleName.Length > 50)
                {
                    return new ResponseDto<PutPersonResponseDto>()
                    {
                        Status = HttpStatusCode.BadRequest,
                        Message = "El Segundo Nombre excede los 50 caracteres",
                        Data = null
                    };
                }

                if (request.NewEmail != null && request.NewEmail.Length > 50)
                {
                    return new ResponseDto<PutPersonResponseDto>()
                    {
                        Status = HttpStatusCode.BadRequest,
                        Message = "El Email excede los 50 caracteres",
                        Data = null
                    };
                }

                if (request.NewAddress != null && request.NewAddress.Length > 50)
                {
                    return new ResponseDto<PutPersonResponseDto>()
                    {
                        Status = HttpStatusCode.BadRequest,
                        Message = "La dirección excede los 50 caracteres",
                        Data = null
                    };
                }

                if (request.NewBirthday == DateTime.MinValue)
                {
                    return new ResponseDto<PutPersonResponseDto>()
                    {
                        Status = HttpStatusCode.BadRequest,
                        Message = "La fecha de nacimiento debe ser valida",
                        Data = null
                    };
                }

                if (request.NewGenderId != null && _dbContext.Genders.FirstOrDefault(g => g.GenderId == request.NewGenderId) == null)
                {
                    return new ResponseDto<PutPersonResponseDto>()
                    {
                        Status = HttpStatusCode.BadRequest,
                        Message = "El IdGenero debe ser valido",
                        Data = null
                    };
                }

                #endregion

                if (request.NewLastName != null) request.NewLastName = request.NewLastName.ToUpper();
                if(request.NewFirstName != null) request.NewFirstName = request.NewFirstName.ToUpper();
                if (request.NewMiddleName != null) request.NewMiddleName = request.NewMiddleName.ToUpper();

                PutPersonResponseDto response = _repository.PutPerson(person, request);

                await _logServices.CreateLog(new CreateLogRequestDto()
                {
                    LogDate = DateTime.Now,
                    LogType = "PERSON",
                    LogProcess = "UPDATE",
                    LogEntity = person.LastName,
                    LogEntityId = person.PersonsId,
                    LogMessage = ""
                });


                return new ResponseDto<PutPersonResponseDto>()
                {
                    Status = HttpStatusCode.OK,
                    Message = "Person successfully updated",
                    Data = response
                };
            }
            else
            {
                return new ResponseDto<PutPersonResponseDto>()
                {
                    Status = HttpStatusCode.BadRequest,
                    Message = "Person not found"
                };
            }
        }

        public async Task<ResponseDto<DeletePersonResponseDto>> DeletePerson(int id)
        {
            Person? person = _repository.GetPersonById(id);

            if (person != null)
            {
                person.Active = false;
                _dbContext.SaveChanges();

                await _logServices.CreateLog(new CreateLogRequestDto()
                {
                    LogDate = DateTime.Now,
                    LogType = "PERSON",
                    LogProcess = "DELETE",
                    LogEntity = person.LastName,
                    LogEntityId = person.PersonsId,
                    LogMessage = ""
                });

                return new ResponseDto<DeletePersonResponseDto>()
                {
                    Status = HttpStatusCode.OK,
                    Message = "Person successfully deleted",
                    Data = new DeletePersonResponseDto()
                    {
                        PersonsId = person.PersonsId,
                        GenderId = person.GenderId,
                        Gender = _repository.GetGenderDescriptionByGenderId(person.GenderId),
                        LastName = person.LastName,
                        FirstName = person.FirstName,
                        MiddleName = person.MiddleName,
                        Birthday = person.Birthday,
                        Email = person.Email,
                        Address = person.Address
                    }
                };
            }
            else
            {
                return new ResponseDto<DeletePersonResponseDto>()
                {
                    Status = HttpStatusCode.NotFound,
                    Message = "Person not found",
                    Data = null
                };
            }
        }

        public async Task<ResponseDto<List<GetPersonResponseDto>>> GetAllPersons()
        {
            return new ResponseDto<List<GetPersonResponseDto>>()
            {
                Status = HttpStatusCode.OK,
                Message = "Persons list",
                Data = _repository.mapPersonsListToPersonResponseDtoList(_repository.GetAllPersons())
            };
        }

        public async Task<ResponseDto<List<GetPersonResponseDto>>> GetAllActivePersons()
        {
            return new ResponseDto<List<GetPersonResponseDto>>()
            {
                Status = HttpStatusCode.OK,
                Message = "Persons list",
                Data = _repository.mapPersonsListToPersonResponseDtoList(_repository.GetAllActivePersons())
            };
        }
    }
}
