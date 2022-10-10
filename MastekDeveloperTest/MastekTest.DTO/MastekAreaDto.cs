using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace MastekDeveloperTest.DTO
{
    public class MastekAreaDto
    {
        public string Country { get; set; }
        public string Region { get; set; }
        [JsonProperty(PropertyName = "admin_district")]
        public string AdminDistrict { get; set; }
        [JsonProperty(PropertyName = "parliamentary_constituency")]
        public string ParlimentaryConstituency { get; set; }
        public double Latitude { get; set; }
        public string Area { get; set; }
    }

    public class MastekAreaPostal
    {
        public IList<MastekAreaDto> Result { get; set; }
    }
    public class MastekAreaAutoCompletion
    {
       public IList<string> Result { get;set; }
    }
}
