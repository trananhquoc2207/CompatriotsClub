using Newtonsoft.Json;

namespace Service.common
{
    public class CommonFilter
    {
        [JsonProperty("fromDate")]
        public DateTime FromDate { get; set; } = DateTime.Now;

        [JsonProperty("toDate")]
        public DateTime ToDate { get; set; } = DateTime.Now;

        [JsonProperty("pageIndex")]
        public int PageIndex { get; set; } = 0;

        [JsonProperty("pageSize")]
        public int PageSize { get; set; } = 10;
    }

    public class PagingFilter
    {
        [JsonProperty("pageIndex")]
        public int PageIndex { get; set; } = 0;

        [JsonProperty("pageSize")]
        public int PageSize { get; set; } = 10;
    }

    public class DateTimeFilter
    {
        [JsonProperty("fromDate")]
        public DateTime FromDate { get; set; } = DateTime.Now;

        [JsonProperty("toDate")]
        public DateTime ToDate { get; set; } = DateTime.Now;
    }
}
