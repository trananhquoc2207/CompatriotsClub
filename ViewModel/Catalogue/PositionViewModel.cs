using Data.Enum;
using ViewModel.common;

namespace ViewModel
{
#nullable disable
    public class PositionViewModel
    {
        public string Name { get; set; }
        public string Note { get; set; }
        public string Description { get; set; }
        public PositionType PositionType { get; set; }
    }
    public class PositionResponseViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Note { get; set; }
        public string Description { get; set; }
        public PositionType PositionType { get; set; }
    }

    public class PositionFilter : PagingFilter
    {
        public string Keyword { get; set; }
    }
}
