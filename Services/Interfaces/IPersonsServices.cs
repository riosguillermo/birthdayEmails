using cumples.DataModel.Dtos.Persons;
using cumples.DataModel.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cumples.Services.Interfaces
{
    public interface IPersonsServices
    {
        Task<ResponseDto<GetPersonResponseDto>> GetPersonById(int id);
        Task<ResponseDto<CreatePersonResponseDto>> CreatePerson(CreatePersonRequestDto request);
        Task<ResponseDto<PutPersonResponseDto>> PutPerson(PutPersonRequestDto request);
        Task<ResponseDto<DeletePersonResponseDto>> DeletePerson(int id);
        Task<ResponseDto<List<GetPersonResponseDto>>> GetAllPersons();
        Task<ResponseDto<List<GetPersonResponseDto>>> GetAllActivePersons();
    }
}
