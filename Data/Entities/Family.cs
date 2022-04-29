#nullable disable

namespace CompatriotsClub.Data
{
    public partial class Family
    {
        public Family()
        {
            //  Members = new HashSet<Member>();
        }

        public int Id { get; set; }
        public int IdMember { get; set; }

        //  public virtual ICollection<Member> Members { get; set; }
    }
}
