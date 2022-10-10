using MastekDeveloperTest.DTO;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MastekDeveloperTest.Service
{
    public interface IPostCodeService
    {
        Task<IList<string>> GetPostCodeForAutoCompletion(string searchStr);
        Task<IList<MastekAreaDto>> GetPostCodeDetails(string code);
    }
}
