using System.ComponentModel.DataAnnotations;

#nullable disable

namespace CompatriotsClub.Data
{
    public partial class FundMember
    {
        [Key]
        public int Id { get; set; }
        public DateTime CreateDate { get; set; }
        public bool Status { get; set; }
        public double Total { get; set; }
        public int MemberId { get; set; }
        public int FundId { get; set; }
        public virtual Fund Fund { get; set; }
        public virtual Member Member { get; set; }

    }
}
