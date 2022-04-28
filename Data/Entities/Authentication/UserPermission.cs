using System.ComponentModel.DataAnnotations;

namespace CompatriotsClub.Data
{
    public class UserPermission
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

        public Guid UserId { get; set; }

        public Guid PermissionId { get; set; }

        public virtual AppUser User { get; set; }

        public virtual Permission Permission { get; set; }
    }
}
