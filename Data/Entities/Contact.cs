#nullable disable

namespace CompatriotsClub.Data
{
    public partial class Contact
    {
        public Contact()
        {
            ContactMembers = new HashSet<ContactMembers>();
        }

        public int Id { get; set; }
        public string Name { get; set; }

        public string Description { get; set; }
        public string Note { get; set; }
        public virtual ICollection<ContactMembers> ContactMembers { get; set; }
    }
}
