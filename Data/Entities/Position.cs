#nullable disable

using Data.Enum;

namespace CompatriotsClub.Data
{
    public partial class Position
    {
        public Position()
        {
            PositionMembers = new HashSet<PositionMember>();
            ContactMembers = new HashSet<ContactMembers>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public PositionType PositionType { get; set; }
        public virtual ICollection<PositionMember> PositionMembers { get; set; }
        public virtual ICollection<ContactMembers> ContactMembers { get; set; }
    }
}
