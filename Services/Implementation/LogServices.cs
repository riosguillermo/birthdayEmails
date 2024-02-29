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
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace cumples.Services.Implementation
{
    public class LogServices : ILogServices
    {
        #region Constructor
        private readonly CumplesContext _dbContext;
        private LogRepository _repository;

        public LogServices(CumplesContext cumplesContext)
        {
            _dbContext = cumplesContext;
            _repository = new LogRepository(_dbContext);
        }
        #endregion Constructor

        public async Task<ResponseDto<CreateLogResponseDto>> CreateLog(CreateLogRequestDto request)
        {
            #region Validaciones
            if (request.LogDate == DateTime.MinValue)
            {
                return new ResponseDto<CreateLogResponseDto>()
                {
                    Status = HttpStatusCode.BadRequest,
                    Message = "Fecha de Log invalida",
                    Data = null
                };
            }

            if (string.IsNullOrEmpty(request.LogType))
            {
                return new ResponseDto<CreateLogResponseDto>()
                {
                    Status = HttpStatusCode.BadRequest,
                    Message = "El tipo de Log es obligatorio",
                    Data = null
                };
            }
            if (request.LogType.Length > 10)
            {
                return new ResponseDto<CreateLogResponseDto>()
                {
                    Status = HttpStatusCode.BadRequest,
                    Message = "El tipo de Log excede los 10 caracteres",
                    Data = null
                };
            }


            if (string.IsNullOrEmpty(request.LogProcess))
            {
                return new ResponseDto<CreateLogResponseDto>()
                {
                    Status = HttpStatusCode.BadRequest,
                    Message = "El proceso de Log es obligatorio",
                    Data = null
                };
            }
            if (request.LogProcess.Length > 50)
            {
                return new ResponseDto<CreateLogResponseDto>()
                {
                    Status = HttpStatusCode.BadRequest,
                    Message = "El proceso de Log excede los 50 caracteres",
                    Data = null
                };
            }


            if (string.IsNullOrEmpty(request.LogEntity))
            {
                return new ResponseDto<CreateLogResponseDto>()
                {
                    Status = HttpStatusCode.BadRequest,
                    Message = "La entidad de Log es obligatorio",
                    Data = null
                };
            }
            if (request.LogEntity.Length > 50)
            {
                return new ResponseDto<CreateLogResponseDto>()
                {
                    Status = HttpStatusCode.BadRequest,
                    Message = "La entidad de Log excede los 50 caracteres",
                    Data = null
                };
            }

            if(request.LogEntityId <= 0)
            {
                return new ResponseDto<CreateLogResponseDto>()
                {
                    Status = HttpStatusCode.BadRequest,
                    Message = "El id de la entidad del Log debe ser valido",
                    Data = null
                };
            }

            #endregion

            request.LogType = request.LogType.ToUpper();
            request.LogProcess = request.LogProcess.ToUpper();
            request.LogEntity = request.LogEntity.ToUpper();
            if(request.LogMessage != null) request.LogMessage = request.LogMessage.ToUpper();

            


            return new ResponseDto<CreateLogResponseDto>()
            {
                Status = HttpStatusCode.OK,
                Message = "Log successfully created",
                Data = _repository.mapLogToCreateLogResponseDto(_repository.CreateLog(request))
            };
        }

        public async Task<ResponseDto<GetLogResponseDto>> GetLogById(int id)
        {
            Log? log = _repository.GetLogById(id);

            if(log != null)
            {
                return new ResponseDto<GetLogResponseDto>()
                {
                    Status = HttpStatusCode.OK,
                    Message = "Log found",
                    Data = _repository.mapLogToGetLogResponseDto(log)
                };
            }
            else
            {
                return new ResponseDto<GetLogResponseDto>()
                {
                    Status = HttpStatusCode.NotFound,
                    Message = "Log not found",
                    Data = null
                };
            }
        }

        public async Task<ResponseDto<PutLogResponseDto>> PutLog(PutLogRequestDto request)
        {
            Log? log = _repository.GetLogById(request.LogId);

            if(log != null)
            {
                #region Validaciones
                if (request.NewLogDate != null && request.NewLogDate == DateTime.MinValue)
                {
                    return new ResponseDto<PutLogResponseDto>()
                    {
                        Status = HttpStatusCode.BadRequest,
                        Message = "Fecha de Log invalida",
                        Data = null
                    };
                }

                if (request.NewLogType != null && request.NewLogType.Length > 10)
                {
                    return new ResponseDto<PutLogResponseDto>()
                    {
                        Status = HttpStatusCode.BadRequest,
                        Message = "El tipo de Log excede los 10 caracteres",
                        Data = null
                    };
                }

                if (request.NewLogProcess != null && request.NewLogProcess.Length > 50)
                {
                    return new ResponseDto<PutLogResponseDto>()
                    {
                        Status = HttpStatusCode.BadRequest,
                        Message = "El proceso de Log excede los 50 caracteres",
                        Data = null
                    };
                }

                if (request.NewLogEntity != null && request.NewLogEntity.Length > 50)
                {
                    return new ResponseDto<PutLogResponseDto>()
                    {
                        Status = HttpStatusCode.BadRequest,
                        Message = "La entidad de Log excede los 50 caracteres",
                        Data = null
                    };
                }

                if (request.NewLogEntityId != null && request.NewLogEntityId <= 0)
                {
                    return new ResponseDto<PutLogResponseDto>()
                    {
                        Status = HttpStatusCode.BadRequest,
                        Message = "El id de la entidad del Log debe ser valido",
                        Data = null
                    };
                }

                #endregion

                if (request.NewLogType != null) request.NewLogType = request.NewLogType.ToUpper();
                if (request.NewLogProcess != null) request.NewLogProcess = request.NewLogProcess.ToUpper();
                if (request.NewLogEntity != null) request.NewLogEntity = request.NewLogEntity.ToUpper();
                if (request.NewLogMessage != null) request.NewLogMessage = request.NewLogMessage.ToUpper();

                return new ResponseDto<PutLogResponseDto>()
                {
                    Status = HttpStatusCode.OK,
                    Message = "Log successfully updated",
                    Data =  _repository.putLog(log, request)
                };
            }
            else
            {
                return new ResponseDto<PutLogResponseDto>()
                {
                    Status = HttpStatusCode.NotFound,
                    Message = "Log not found",
                    Data = null
                };
            }
        }

        public async Task<ResponseDto<List<GetLogResponseDto>>> GetAllLogs()
        {
            return new ResponseDto<List<GetLogResponseDto>>()
            {
                Status = HttpStatusCode.OK,
                Message = "Log list",
                Data = _repository.mapLogListToGetLogResponseDtoList(_repository.GetAllLogs())
            };
        }

        public async Task<ResponseDto<List<GetLogResponseDto>>> GetLastLogs(int quantity)
        {
            return new ResponseDto<List<GetLogResponseDto>>()
            {
                Status = HttpStatusCode.OK,
                Message = "Log list",
                Data = _repository.mapLogListToGetLogResponseDtoList(_repository.GetLastLogs(quantity))
            };
        }
    }
}
