using Azure.Core;
using cumples.DataModel.Dtos.Gender;
using cumples.DataModel.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;

namespace cumples.Infrastructure.Repository
{
    public class GenderRepository
    {
        #region Constructor
        private readonly CumplesContext _dbContext;

        public GenderRepository(
            CumplesContext cumplesContext
            )
        {
            _dbContext = cumplesContext;
        }
        #endregion Constructor
        public Gender? GetGenderByID(int id)
        {
            return _dbContext.Genders.FirstOrDefault(g => g.GenderId == id);
        }

        public List<Gender> GetAllGenders()
        {
            return _dbContext.Genders.ToList();
        }

        public List<Gender> GetAllActiveGenders()
        {
            return _dbContext.Genders.Where(g => g.Active == true).ToList();
        }

        public CreateGenderResponseDto MapGenderToCreateGenderResponseDto(Gender gender)
        {
            return new CreateGenderResponseDto
            {
                GenderId = gender.GenderId,
                Description = gender.Description,
                Prefix = gender.Prefix,
                Active = gender.Active
            };
        }

        public GetGenderResponseDto MapGenderToGetGenderResponseDto(Gender gender)
        {
            return new GetGenderResponseDto
            {
                GenderId = gender.GenderId,
                Description = gender.Description,
                Prefix = gender.Prefix,
                Active = gender.Active
            };
        }

        public List<GetGenderResponseDto> MapGenderListToGetGenderResponseDtoList(List<Gender> genders)
        {
            List<GetGenderResponseDto> list = new List<GetGenderResponseDto>();
            foreach(Gender gender in genders) {
                list.Add(MapGenderToGetGenderResponseDto(gender));
            }
            return list;
        }

        public Gender CreateGender(CreateGenderRequestDto newGender)
        {
            Gender gender = new Gender()
            {
                Description = newGender.Description,
                Prefix = newGender.Prefix,
                CreatedDate = DateTime.Now,
                CreatedBy = "test",
                ModifiedBy = "test",
                ModifiedDate = DateTime.Now,
                Active = newGender.Active
            };

            _dbContext.Genders.Add(gender);
            _dbContext.SaveChanges();

 
            return gender;
        }

        public PutGenderResponseDto PutGender(Gender gender, PutGenderRequestDto newGender)
        {
            PutGenderResponseDto response = new PutGenderResponseDto()
            {
                GenderId = gender.GenderId,
                Description = gender.Description,
                Prefix = gender.Prefix,
                Active = gender.Active.ToString()
            };
            if(newGender.NewDescription != null && gender.Description != newGender.NewDescription)
            {
                response.Description = gender.Description + " --> " + newGender.NewDescription;
                gender.Description = newGender.NewDescription;
            }
            if(newGender.NewPrefix != null && gender.Prefix != newGender.NewPrefix)
            {
                response.Prefix = gender.Prefix + " --> " + newGender.NewPrefix;
                gender.Prefix = newGender.NewPrefix;
            }
            if(newGender.NewActive != null && gender.Active != newGender.NewActive)
            {
                response.Active = gender.Active.ToString() + " --> " + newGender.NewActive;
                gender.Active = newGender.NewActive;
            }

            gender.ModifiedDate = DateTime.Now;

            _dbContext.SaveChanges();

            return response;
        }
    }
}
