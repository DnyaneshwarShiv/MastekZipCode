using Newtonsoft.Json;
using System;

namespace MastekTest.DomainModel
{
    public class MastekAreaModel
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
}
