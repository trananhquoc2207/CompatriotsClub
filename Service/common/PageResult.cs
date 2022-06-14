using Newtonsoft.Json;

namespace Service.common
{
#nullable disable
    public class PagingModel
    {
        [JsonProperty("totalCounts")]
        public dynamic TotalCounts { get; set; }

        [JsonProperty("data")]
        public dynamic Data { get; set; }
    }

    public class PagingModel<T>
    {
        [JsonProperty("totalCounts")]
        public dynamic TotalCounts { get; set; }

        [JsonProperty("data")]
        public List<T> Data { get; set; }
    }
    public class ResultModel
    {
        [JsonProperty("succeed")]
        public bool Succeed { get; set; }

        [JsonProperty("errorMessages")]
        public string ErrorMessages { get; set; }

        [JsonProperty("data")]
        public dynamic Data { get; set; }
    }
}
