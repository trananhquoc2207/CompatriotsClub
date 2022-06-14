using Newtonsoft.Json;

namespace ViewModel.common
{
#nullable disable
    public class PageResult
    {
        [JsonProperty("totalCounts")]
        public dynamic TotalCounts { get; set; }

        [JsonProperty("data")]
        public dynamic Data { get; set; }
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
