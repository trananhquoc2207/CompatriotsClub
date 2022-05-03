#nullable disable

namespace CompatriotsClub.Data
{
    public partial class PositionMember
    {
        public int MemberId { get; set; }
        public int RoleId { get; set; }
        public virtual Member Member { get; set; }
        public virtual Position Position { get; set; }
    }
}
