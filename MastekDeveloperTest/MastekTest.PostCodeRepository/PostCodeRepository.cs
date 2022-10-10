using MastekDeveloperTest.PostCodeRepository;
using Microsoft.Extensions.Configuration;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace MastekTest.PostCodeRepository
{
    public class PostCodesRepository : IPostCodeRepository
    {
        private readonly HttpClient httpClient;
        public PostCodesRepository( IConfiguration configuration)
        {
            httpClient = new HttpClient();
            httpClient.BaseAddress= new Uri(configuration[ZipConstant.BaseUrl]);
            httpClient.DefaultRequestHeaders.Accept.Clear();
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(ZipConstant.HeaderValue));
        }
        /// <summary>
        /// Get data based on relative url.
        /// </summary>
        /// <param name="relativeUrl"></param>
        /// <returns></returns>
        public async Task<HttpResponseMessage> Get(string relativeUrl)
        {
            return await httpClient.GetAsync(relativeUrl);
        }
    }
}
