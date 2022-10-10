using AutoMapper;
using MastekDeveloperTest.DTO;
using MastekDeveloperTest.PostCodeRepository;
using MastekTest.DomainModel;
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
        private readonly IMapper _mapper;

        /// <summary>
        /// constructor
        /// </summary>
        /// <param name="postCodeRepository"></param>
        public PostCodeService(IPostCodeRepository postCodeRepository, IMapper mapper)
        {
            _postCodeRepository = postCodeRepository;
            _mapper = mapper;
        }
        #region public method
        /// <summary>
        /// Get details based on zip code
        /// </summary>
        /// <param name="code">zip code</param>
        /// <returns></returns>
        public async Task<IList<MastekAreaDto>> GetPostCodeDetails(string code)
        {
            string relPath = $"postcodes?q={code}";
            string area = await GetResponseFromClient(relPath);
            var searchList = JsonConvert.DeserializeObject<MastekAreaPostal>(area);
            var zipAreaDetails= searchList?.Result?.Select(s=> new MastekAreaModel()
            {
                Area = s.Latitude< 52.229466?ZipConstant.AreaSouth: (52.229466<=s.Latitude && s.Latitude< 53.27169? ZipConstant.AreaMidland: ZipConstant.AreaNorth),
                Latitude = s.Latitude,
                AdminDistrict = s.AdminDistrict,
                Country = s.Country,
                ParlimentaryConstituency = s.ParlimentaryConstituency,
                Region  =s.Region
            }).ToList();
            return _mapper.Map<IList<MastekAreaDto>>(zipAreaDetails);
        }

        /// <summary>
        /// Get zip code based on type ahead string
        /// </summary>
        /// <param name="searchStr"></param>
        /// <returns></returns>
        public async Task<IList<string>> GetPostCodeForAutoCompletion(string searchStr)
        {
            string relPath = $"/postcodes/{searchStr}/autocomplete";
            string area = await GetResponseFromClient(relPath);
            var searchList= JsonConvert.DeserializeObject<MastekAreaAutoCompletion>(area)?.Result;
            return searchList;
        }
        #endregion

        #region private method
        /// <summary>
        /// Get response
        /// </summary>
        /// <param name="relPath"></param>
        /// <returns></returns>
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

        #endregion
    }
}
