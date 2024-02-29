using cumples.DataModel.Dtos.Gender;
using cumples.DataModel.Entity;
using cumples.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace cumples.API.Controllers
{
    public class GenderController : Controller
    {
        #region Constructor
        private readonly IGenderServices _genderServices;

        public GenderController(
            IGenderServices genderServices)
        {
            _genderServices = genderServices;
        }
        #endregion Constructor

        [HttpPost("CreateGender")]
        public async Task<ResponseDto<CreateGenderResponseDto>> CreateGender(CreateGenderRequestDto request)
        {
            return await _genderServices.CreateGender(request);
        }

        [HttpGet("GetGenderByID")]
        public async Task<ResponseDto<GetGenderResponseDto>> GetGenderByID(int id)
        {
            return await _genderServices.GetGenderByID(id);
        }

        [HttpPut("PutGender")]
        public async Task<ResponseDto<PutGenderResponseDto>> PutGender(PutGenderRequestDto request)
        {
            return await _genderServices.PutGender(request);
        }

        [HttpDelete("DeleteGender")]
        public async Task<ResponseDto<DeleteGenderResponseDto>> DeleteGender(int id)
        {
            return await _genderServices.DeleteGender(id);
        }

        [HttpGet("GetAllGenders")]
        public async Task<ResponseDto<List<GetGenderResponseDto>>> GetAllGenders()
        {
            return await _genderServices.GetAllGenders();
        }

        [HttpGet("GetAllActiveGenders")]
        public async Task<ResponseDto<List<GetGenderResponseDto>>> GetAllActiveGenders()
        {
            return await _genderServices.GetAllActiveGenders();
        }
    }
    }
