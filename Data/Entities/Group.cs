﻿#nullable disable

namespace CompatriotsClub.Data
{
    public partial class Group
    {
        public Group()
        {
            Members = new HashSet<Member>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Region { get; set; }
        public string Description { get; set; }
        public int? IdMember { get; set; }

        public virtual ICollection<Member> Members { get; set; }
    }
}
