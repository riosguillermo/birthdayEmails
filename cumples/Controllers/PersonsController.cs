using cumples.DataModel.Dtos.Persons;
using cumples.DataModel.Entity;
using cumples.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;

namespace cumples.API.Controllers
{
    public class PersonsController : Controller
    {
        #region Constructor
        private readonly IPersonsServices _personsServices;

        public PersonsController(
            IPersonsServices personsServices)
        {
            _personsServices = personsServices;
        }
        #endregion Constructor

        [HttpPost("CreatePerson")]
        public async Task<ResponseDto<CreatePersonResponseDto>> CreatePerson(CreatePersonRequestDto request)
        {
            return await _personsServices.CreatePerson(request);
        }

        [HttpGet("GetPersonById")]
        public async Task<ResponseDto<GetPersonResponseDto>> GetPersonById(int id)
        {
            return await _personsServices.GetPersonById(id);
        }
       
        [HttpPut("PutPerson")]
        public async Task<ResponseDto<PutPersonResponseDto>> PutPerson(PutPersonRequestDto request)
        {
            return await _personsServices.PutPerson(request);
        }

        [HttpDelete("DeletePerson")]
        public async Task<ResponseDto<DeletePersonResponseDto>> DeletePerson(int id)
        {
            return await _personsServices.DeletePerson(id);
        }

        [HttpGet("GetAllPersons")]
        public async Task<ResponseDto<List<GetPersonResponseDto>>> GetAllPersons()
        {
            return await _personsServices.GetAllPersons();
        }

        [HttpGet("GetAllActivePersons")]
        public async Task<ResponseDto<List<GetPersonResponseDto>>> GetAllActivePersons()
        {
            return await _personsServices.GetAllActivePersons();
        }
    }
}
 