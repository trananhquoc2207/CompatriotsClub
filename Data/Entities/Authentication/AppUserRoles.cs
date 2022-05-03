using Microsoft.AspNetCore.Identity;

namespace CompatriotsClub.Data
{
#nullable disable
    public class AppUserRoles : IdentityUserRole<Guid>
    {
        public virtual AppUser User { get; set; }
        public virtual AppRole Role { get; set; }
    }
}
