namespace ViewModel.System
{
#nullable disable
    public class RoleAddModel
    {
        public string Name { get; set; }
    }

    public class RoleUpdateModel : RoleAddModel
    {
    }

    public class RoleModel : RoleUpdateModel
    {
        public Guid Id { get; set; }
    }

    public class RoleDetailModel : RoleModel
    {
        public List<MinifyUserModel> Users { get; set; }
    }

    public class AddUserToRoleModel
    {
        public List<Guid> Ids { get; set; }
    }

    public class RemoveUserOfRoleModel
    {
        public List<Guid> Ids { get; set; }
    }

    public class AddPermissionToRoleModel
    {
        public List<Guid> Ids { get; set; }
    }

    public class RemovePermissionOfRoleModel
    {
        public List<Guid> Ids { get; set; }
    }

    #region Utilities
    public class RoleWithLevel
    {
        public string Role { get; set; }

        public int Level { get; set; }
    }

    public class MinifyUserModel
    {
        public Guid Id { get; set; }

        public String Username { get; set; }

    }
    #endregion
}
