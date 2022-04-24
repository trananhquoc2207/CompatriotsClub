using Microsoft.AspNetCore.Identity;

#nullable disable

namespace CompatriotsClub.Data
{
    public partial class AppUser : IdentityUser<int>
    {
        public AppUser()
        {

            MemberUsers = new HashSet<MemberUser>();
            Posts = new HashSet<Post>();
        }


        public bool? ActiveAccount { get; set; }
        public string Avatar { get; set; }
        public bool IsDeleted { get; set; } = false;



        public virtual ICollection<MemberUser> MemberUsers { get; set; }
        public virtual ICollection<Post> Posts { get; set; }

        public ICollection<Room> Rooms { get; set; }
        public ICollection<Message> Messages { get; set; }
    }
}
