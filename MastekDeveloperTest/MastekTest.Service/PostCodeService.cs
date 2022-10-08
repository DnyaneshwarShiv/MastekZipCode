using MastekDeveloperTest.DTO;
using MastekDeveloperTest.PostCodeRepository;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MastekDeveloperTest.Service
{
    public class PostCodeService : IPostCodeService
    {
        private readonly IPostCodeRepository _postCodeRepository;

        public PostCodeService(IPostCodeRepository postCodeRepository)
        {
            _postCodeRepository = postCodeRepository;
        }
        public async Task<IList<MastekArea>> GetPostCodeDetails(string code)
        {
            string relPath = $"postcodes?q={code}";
            string area = await GetResponseFromClient(relPath);
            var searchList = JsonConvert.DeserializeObject<MastekAreaPostal>(area);
            return searchList?.Result?.Select(s=> new MastekArea()
            {
                Area = s.Latitude< 52.229466?"South": (52.229466<=s.Latitude && s.Latitude< 53.27169? "Midlands": "North"),
                Latitude = s.Latitude,
                AdminDistrict = s.AdminDistrict,
                Country = s.Country,
                ParlimentaryConstituency = s.ParlimentaryConstituency,
                Region  =s.Region
            }).ToList();
        }

        private async Task<string> GetResponseFromClient(string relPath)
        {
            var response = await _postCodeRepository.Get(relPath);
            if (!response.IsSuccessStatusCode)
            {
                throw new Exception("Error");
            }
            var area = await response.Content.ReadAsStringAsync();
            return area;
        }

        public async Task<IList<string>> GetPostCodeForAutoCompletion(string searchStr)
        {
            string relPath = $"/postcodes/{searchStr}/autocomplete";
            string area = await GetResponseFromClient(relPath);
            var searchList= JsonConvert.DeserializeObject<MastekAreaAutoCompletion>(area)?.Result;
            return searchList;
        }
    }
}
