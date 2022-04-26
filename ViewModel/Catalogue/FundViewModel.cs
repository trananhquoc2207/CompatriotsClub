namespace ViewModel.Catalogue
{
#nullable disable
    public class FundViewModel
    {
        public string Name { get; set; }
        public double? TotalFund { get; set; }
        public DateTime? CreatedDate { get; set; } = DateTime.Now;
        public string Description { get; set; }
    }
    public class FundResponseViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double? TotalFund { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string Description { get; set; }
    }
}
