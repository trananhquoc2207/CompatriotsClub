#nullable disable

namespace CompatriotsClub.Data
{
    public partial class Fund
    {
        public Fund()
        {
            FundMembers = new HashSet<FundMember>();
            FundGroups = new HashSet<FundGroup>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public double? TotalFund { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string Description { get; set; }

        public virtual ICollection<FundMember> FundMembers { get; set; }
        public virtual ICollection<FundGroup> FundGroups { get; set; }
    }
}
