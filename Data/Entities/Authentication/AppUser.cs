using CompatriotsClub.Entities;
using Microsoft.AspNetCore.Identity;

#nullable disable

namespace CompatriotsClub.Data
{
    public partial class AppUser : IdentityUser<Guid>
    {
        public AppUser()
        {

            MemberUsers = new HashSet<MemberUser>();
        }

        public bool? ActiveAccount { get; set; }
        public string Avatar { get; set; }
        public bool IsDeleted { get; set; } = false;
        public virtual ICollection<MemberUser> MemberUsers { get; set; }
        public virtual List<Feel> Feel { get; set; }
        public virtual List<Post> Albums { get; set; }
        public virtual List<Comment> Conments { get; set; }

        public ICollection<AppUserRoles> UserRoles { get; set; }
    }
}
