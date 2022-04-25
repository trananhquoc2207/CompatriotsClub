using Microsoft.AspNetCore.Identity;

#nullable disable

namespace CompatriotsClub.Data
{
    public class AppRole : IdentityRole<int>
    {
        public string Description { get; set; }
    }
}
