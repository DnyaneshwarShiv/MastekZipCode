using MastekDeveloperTest.PostCodeRepository;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace MastekTest.PostCodeRepository
{
    public class PostCodesRepository : IPostCodeRepository
    {
        private readonly HttpClient httpClient;
        public PostCodesRepository()
        {
            httpClient = new HttpClient();
            httpClient.BaseAddress= new Uri("https://postcodes.io/");
            httpClient.DefaultRequestHeaders.Accept.Clear();
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }
        public async Task<HttpResponseMessage> Get(string relativeUrl)
        {
            return await httpClient.GetAsync(relativeUrl);
        }
    }
}
