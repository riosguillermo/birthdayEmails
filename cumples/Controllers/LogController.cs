using cumples.DataModel.Dtos.Log;
using cumples.DataModel.Entity;
using cumples.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace cumples.API.Controllers
{
    public class LogController : Controller
    {
        #region Constructor
        private readonly ILogServices _logServices;

        public LogController(
            ILogServices logServices)
        {
            _logServices = logServices;
        }
        #endregion Constructor

        [HttpPost("CreateLog")]
        public async Task<ResponseDto<CreateLogResponseDto>> CreateLog(CreateLogRequestDto request)
        {
            return await _logServices.CreateLog(request);
        }

        [HttpGet("GetLog")]
        public async Task<ResponseDto<GetLogResponseDto>> GetLogById(int id)
        {
            return await _logServices.GetLogById(id);
        }

        [HttpPut("PutLog")]
        public async Task<ResponseDto<PutLogResponseDto>> PutLog(PutLogRequestDto request)
        {
            return await _logServices.PutLog(request);
        }

        [HttpGet("GetAllLogs")]
        public async Task<ResponseDto<List<GetLogResponseDto>>> GetAllLogs()
        {
            return await _logServices.GetAllLogs();
        }

        [HttpGet("GetLastLogs")]
        public async Task<ResponseDto<List<GetLogResponseDto>>> GetLastLogs(int quantity)
        {
            return await _logServices.GetLastLogs(quantity);
        }
    }
}
