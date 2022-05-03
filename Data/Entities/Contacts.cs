#nullable disable

namespace CompatriotsClub.Data
{
    public partial class Contacts
    {
        public Contacts()
        {
            ContactMembers = new HashSet<ContactMembers>();
        }

        public int Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool IsDelete { get; set; }
        public virtual ICollection<ContactMembers> ContactMembers { get; set; }
    }
}
