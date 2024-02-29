using cumples.DataModel.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cumples.Services.Interfaces
{
    public interface IBirthdayServices
    {
       Task<ResponseDto<string>> SendTodayBirthdayEmails();
    }
}
