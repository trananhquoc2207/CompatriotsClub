using System.ComponentModel.DataAnnotations;

namespace CompatriotsClub.Data
{
    public class RolePermission
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

        public Guid RoleId { get; set; }

        public Guid PermissionId { get; set; }

        public virtual AppRole Role { get; set; }

        public virtual Permission Permission { get; set; }
    }
}
