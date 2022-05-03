using Microsoft.AspNetCore.Identity;

#nullable disable

namespace CompatriotsClub.Data
{
    public class AppRole : IdentityRole<Guid>
    {
        public string Description { get; set; }

        public ICollection<AppUserRoles> UserRoles { get; set; }
    }
}
