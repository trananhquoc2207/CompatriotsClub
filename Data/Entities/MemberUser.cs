#nullable disable

namespace CompatriotsClub.Data
{
    public partial class MemberUser
    {
        public int MemberId { get; set; }
        public Guid UserId { get; set; }

        public virtual Member Member { get; set; }
        public virtual AppUser User { get; set; }
    }
}
