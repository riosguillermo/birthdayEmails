using Azure;
using Azure.Core;
using cumples.DataModel.Dtos.Log;
using cumples.DataModel.Dtos.Persons;
using cumples.DataModel.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cumples.Infrastructure.Repository
{
    public class LogRepository
    {
        #region Constructor
        private readonly CumplesContext _dbContext;

        public LogRepository(CumplesContext cumplesContext)
        {
            _dbContext = cumplesContext;
        }
        #endregion Constructor

        public Log? GetLogById(int id)
        {
            return _dbContext.Logs.FirstOrDefault(l => l.LogId == id);
        }

        public List<Log> GetAllLogs()
        {
            return _dbContext.Logs.ToList();
        }

        public List<Log> GetLastLogs(int quantity)
        {
            return _dbContext.Logs.OrderByDescending(l => l.LogDate).Take(quantity).ToList();
        }

        public CreateLogResponseDto mapLogToCreateLogResponseDto(Log log)
        {
            return new CreateLogResponseDto()
            {
                LogId = log.LogId,
                LogDate = log.LogDate,
                User = log.User,
                LogType = log.LogType,
                LogProcess = log.LogProcess,
                LogEntity = log.LogEntity,
                LogEntityId = log.LogEntityId,
                LogMessage = log.LogMessage
            };
        }

        public GetLogResponseDto mapLogToGetLogResponseDto(Log log)
        {
            return new GetLogResponseDto()
            {
                LogId = log.LogId,
                LogDate = log.LogDate,
                User = log.User,
                LogType = log.LogType,
                LogProcess = log.LogProcess,
                LogEntity = log.LogEntity,
                LogEntityId = log.LogEntityId,
                LogMessage = log.LogMessage
            };
        }

        public List<GetLogResponseDto> mapLogListToGetLogResponseDtoList(List<Log> logList)
        {
            List<GetLogResponseDto> list = new List<GetLogResponseDto>();
            foreach (Log log in logList)
            {
                list.Add(mapLogToGetLogResponseDto(log));
            }
            return list;
        }

        public Log CreateLog(CreateLogRequestDto newLog)
        {
            Log log = new Log()
            {
                LogDate = newLog.LogDate,
                User = "TEST",
                LogType = newLog.LogType,
                LogProcess = newLog.LogProcess,
                LogEntity = newLog.LogEntity,
                LogEntityId = newLog.LogEntityId,
                LogMessage = newLog.LogMessage,
            };

            _dbContext.Logs.Add(log);
            _dbContext.SaveChanges();

            return log;
        }

        public PutLogResponseDto putLog(Log log, PutLogRequestDto newLog)
        {
            PutLogResponseDto responseDto = new PutLogResponseDto()
            {
                LogId = log.LogId,
                LogDate = log.LogDate.ToShortDateString(),
                User = log.User,
                LogType = log.LogType,
                LogProcess = log.LogProcess,
                LogEntity = log.LogEntity,
                LogEntityId = log.LogEntityId.ToString(),
                LogMessage = log.LogMessage
            };

            if (newLog.NewLogDate != null && newLog.NewLogDate != DateTime.MinValue && log.LogDate != newLog.NewLogDate)
            {
                responseDto.LogDate = log.LogDate.ToShortDateString() + " --> " + newLog.NewLogDate.ToString();
                log.LogDate = (DateTime)newLog.NewLogDate;
            }

            if (newLog.NewLogType != null && log.LogType != newLog.NewLogType)
            {
                responseDto.LogType = log.LogType + " --> " + newLog.NewLogType;
                log.LogType = newLog.NewLogType;
            }
            if (newLog.NewLogProcess != null && log.LogProcess != newLog.NewLogProcess)
            {
                responseDto.LogProcess = log.LogProcess + " --> " + newLog.NewLogProcess;
                log.LogProcess = newLog.NewLogProcess;
            }
            if (newLog.NewLogEntity != null && log.LogEntity != newLog.NewLogEntity)
            {
                responseDto.LogEntity = log.LogEntity + " --> " + newLog.NewLogEntity;
                log.LogEntity = newLog.NewLogEntity;
            }
            if (newLog.NewLogEntityId != null && log.LogEntityId != newLog.NewLogEntityId)
            {
                responseDto.LogEntityId = log.LogEntityId + " --> " + newLog.NewLogEntityId;
                log.LogEntityId = (int)newLog.NewLogEntityId;
            }
            if (newLog.NewLogMessage != null && log.LogMessage != newLog.NewLogMessage)
            {
                responseDto.LogMessage = log.LogMessage + " --> " + newLog.NewLogMessage;
                log.LogMessage = newLog.NewLogMessage;
            }

            _dbContext.SaveChanges();
            return responseDto;
        }
    }
}
