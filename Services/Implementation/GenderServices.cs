using cumples.DataModel.Dtos.Gender;
using cumples.DataModel.Entity;
using cumples.Services.Interfaces;
using cumples.Infrastructure.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using cumples.DataModel.Dtos.Log;
using Azure;

namespace cumples.Services.Implementation
{
    public class GenderServices : IGenderServices
    {
        #region Constructor
        private GenderRepository _repository;
        private readonly CumplesContext _dbContext;
        private ILogServices _logServices;
        

        public GenderServices(CumplesContext cumplesContext, ILogServices logServices)
        {
            _dbContext = cumplesContext;
            _repository = new GenderRepository(_dbContext);
            _logServices = logServices;
        }
        #endregion Constructor

        public async Task<ResponseDto<CreateGenderResponseDto>> CreateGender(CreateGenderRequestDto request)
        {
            #region Validaciones
            if (string.IsNullOrEmpty(request.Description))
            {
                return new ResponseDto<CreateGenderResponseDto>()
                {
                    Status = HttpStatusCode.BadRequest,
                    Message = "El campo Descripción es obligatorio",
                    Data = null
                };
            }

            if(request.Description.Length > 50)
            {
                return new ResponseDto<CreateGenderResponseDto>()
                {
                    Status = HttpStatusCode.BadRequest,
                    Message = "La Descripción excede los 50 caracteres",
                    Data = null
                };
            }

            if(request.Prefix != null && request.Prefix.Length > 10)
            {
                return new ResponseDto<CreateGenderResponseDto>()
                {
                    Status = HttpStatusCode.BadRequest,
                    Message = "El prefijo excede los 10 caracteres",
                    Data = null
                };
            }

            #endregion Validaciones

            request.Description = request.Description.ToUpper();
            if(request.Active == null) request.Active = true;

            if (string.IsNullOrEmpty(request.Prefix))
            {
                if(request.Description.Length >= 3)
                {
                    request.Prefix = request.Description.Substring(0, 3);
                }
                else
                {
                    request.Prefix = request.Description;
                }
            }

            CreateGenderResponseDto response = _repository.MapGenderToCreateGenderResponseDto(_repository.CreateGender(request));

            await _logServices.CreateLog(new CreateLogRequestDto()
            {
                LogDate = DateTime.Now,
                LogType = "GENDER",
                LogProcess = "CREATE",
                LogEntity = response.Description,
                LogEntityId = response.GenderId,
                LogMessage = ""
            });
     
            return new ResponseDto<CreateGenderResponseDto>()
            {
                Status = HttpStatusCode.OK,
                Message = "Gender successfully created",
                Data = response
            };
        }

        public async Task<ResponseDto<GetGenderResponseDto>> GetGenderByID(int id)
        {

            Gender? result = _repository.GetGenderByID(id);

            if(result != null)
            {
                return new ResponseDto<GetGenderResponseDto>()
                {
                    Status = HttpStatusCode.OK,
                    Message = "Gender found",
                    Data = _repository.MapGenderToGetGenderResponseDto(result)
                };
            }
            else
            {
                return new ResponseDto<GetGenderResponseDto>()
                {
                    Status = HttpStatusCode.NotFound,
                    Message = "Gender not found",
                    Data = null
                };
            };
         
        }

        public async Task<ResponseDto<PutGenderResponseDto>> PutGender(PutGenderRequestDto request)
        {

            #region Validaciones
            if (request.NewDescription != null && request.NewDescription.Length > 50)
            {
                return new ResponseDto<PutGenderResponseDto>()
                {
                    Status = HttpStatusCode.BadRequest,
                    Message = "La Descripción excede los 50 caracteres",
                    Data = null
                };
            }

            if (request.NewPrefix != null && request.NewPrefix.Length > 10)
            {
                return new ResponseDto<PutGenderResponseDto>()
                {
                    Status = HttpStatusCode.BadRequest,
                    Message = "El prefijo excede los 10 caracteres",
                    Data = null
                };
            }


            #endregion

            Gender? result = _repository.GetGenderByID(request.GenderId);
            if (result != null)
            {
                if (request.NewDescription != null) request.NewDescription = request.NewDescription.ToUpper();
                if (request.NewPrefix != null) request.NewPrefix = request.NewPrefix.ToUpper();

                PutGenderResponseDto response = _repository.PutGender(result, request);

                await _logServices.CreateLog(new CreateLogRequestDto()
                {
                    LogDate = DateTime.Now,
                    LogType = "GENDER",
                    LogProcess = "UPDATE",
                    LogEntity = result.Description,
                    LogEntityId = result.GenderId,
                    LogMessage = ""
                });

                return new ResponseDto<PutGenderResponseDto>()
                    {
                        Status = HttpStatusCode.OK,
                        Message = "Gender successfully updated",
                        Data = response
                };   
            }
            else
            {
                return new ResponseDto<PutGenderResponseDto>()
                {
                    Status = HttpStatusCode.NotFound,
                    Message = "Gender not found",
                    Data = null
                };
            }          
        }

        public async Task<ResponseDto<DeleteGenderResponseDto>> DeleteGender(int id)
        {

            Gender? result = _repository.GetGenderByID(id);
            
            if (result != null)
            {
                result.Active = false;
                _dbContext.SaveChanges();

                await _logServices.CreateLog(new CreateLogRequestDto()
                {
                    LogDate = DateTime.Now,
                    LogType = "GENDER",
                    LogProcess = "DELETE",
                    LogEntity = result.Description,
                    LogEntityId = result.GenderId,
                    LogMessage = ""
                });

                return new ResponseDto<DeleteGenderResponseDto>()
                {
                    Status = HttpStatusCode.OK,
                    Message = "Gender successfully deleted",
                    Data = new DeleteGenderResponseDto()
                    {
                        GenderId = result.GenderId,
                        Description = result.Description,
                        Prefix = result.Prefix
                    }
                };
            }
            else
            {
                return new ResponseDto<DeleteGenderResponseDto>()
                {
                    Status = HttpStatusCode.NotFound,
                    Message = "Gender not found",
                    Data = null
                };
            }
        }

        public async Task<ResponseDto<List<GetGenderResponseDto>>> GetAllGenders()
        {
            return new ResponseDto<List<GetGenderResponseDto>>()
            {
                Status = HttpStatusCode.OK,
                Message = "Gender List",
                Data = _repository.MapGenderListToGetGenderResponseDtoList(_repository.GetAllGenders())
            };
        }

        public async Task<ResponseDto<List<GetGenderResponseDto>>> GetAllActiveGenders()
        {
            return new ResponseDto<List<GetGenderResponseDto>>()
            {
                Status = HttpStatusCode.OK,
                Message = "Gender List",
                Data = _repository.MapGenderListToGetGenderResponseDtoList(_repository.GetAllActiveGenders())
            };
        }
    }
}
