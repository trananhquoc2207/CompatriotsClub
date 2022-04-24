#nullable disable

namespace CompatriotsClub.Data
{
    public partial class AddressMember
    {
        public int MemberId { get; set; }
        public int AddressId { get; set; }

        public virtual Address Address { get; set; }
        public virtual Member Member { get; set; }
    }
}
