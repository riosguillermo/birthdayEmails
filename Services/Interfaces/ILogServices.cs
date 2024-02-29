using cumples.DataModel.Dtos.Gender;
using cumples.DataModel.Dtos.Log;
using cumples.DataModel.Dtos.Persons;
using cumples.DataModel.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cumples.Services.Interfaces
{
    public interface ILogServices
    {
        Task<ResponseDto<CreateLogResponseDto>> CreateLog (CreateLogRequestDto request);
        Task<ResponseDto<GetLogResponseDto>> GetLogById (int id);
        Task<ResponseDto<PutLogResponseDto>> PutLog(PutLogRequestDto request);
        Task<ResponseDto<List<GetLogResponseDto>>> GetAllLogs();
        Task<ResponseDto<List<GetLogResponseDto>>> GetLastLogs(int quantity);
    }
}
