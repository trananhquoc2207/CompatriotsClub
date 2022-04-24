#nullable disable

namespace CompatriotsClub.Data
{
    public partial class RoleMember
    {
        public int MemberId { get; set; }
        public int RoleId { get; set; }

        public virtual Member Member { get; set; }
        public virtual Roles Role { get; set; }
    }
}
