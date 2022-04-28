using System.ComponentModel.DataAnnotations;

namespace CompatriotsClub.Data
{
    public class Permission
    {
        public Permission()
        {
            UserPermissions = new HashSet<UserPermission>();
        }

        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

        public string Code { get; set; }

        public string Description { get; set; }

        public virtual ICollection<UserPermission> UserPermissions { get; set; }
    }
}
