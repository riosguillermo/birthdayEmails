using cumples.DataModel.Dtos.Gender;
using cumples.DataModel.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cumples.Services.Interfaces
{
    public interface IGenderServices
    {
        Task<ResponseDto<CreateGenderResponseDto>> CreateGender (CreateGenderRequestDto request);
        Task<ResponseDto<GetGenderResponseDto>> GetGenderByID(int id);
        Task<ResponseDto<PutGenderResponseDto>> PutGender (PutGenderRequestDto request);
        Task<ResponseDto<DeleteGenderResponseDto>> DeleteGender(int id);

        Task<ResponseDto<List<GetGenderResponseDto>>> GetAllGenders();
        Task<ResponseDto<List<GetGenderResponseDto>>> GetAllActiveGenders();
    }
}
