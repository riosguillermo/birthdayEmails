using cumples.DataModel.Entity;
using cumples.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace cumples.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BirthdayController : ControllerBase
    {
        #region Constructor
        private readonly IBirthdayServices _birthdayService;

        public BirthdayController(IBirthdayServices birthdayServices)
        {
            _birthdayService = birthdayServices;
        }
        #endregion Constructor


        [HttpGet("SendTodayBirthdayEmails")]
        public async Task<ResponseDto<string>> SendTodayBirthdayEmails()
        {
            return await _birthdayService.SendTodayBirthdayEmails();
        }
    }
}
