using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace MastekDeveloperTest.PostCodeRepository
{
    public interface IPostCodeRepository
    {
        Task<HttpResponseMessage> Get(string relativeUrl);
    }
}
