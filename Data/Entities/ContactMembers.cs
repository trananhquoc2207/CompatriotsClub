#nullable disable

namespace CompatriotsClub.Data
{
    public partial class ContactMembers
    {
        public int RoleId { get; set; }
        public int MemberId { get; set; }
        public int ContactId { get; set; }
        public virtual Position Position { get; set; }
        public virtual Contacts Contact { get; set; }
        public virtual Member Member { get; set; }
    }
}
