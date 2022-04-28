namespace ViewModel.System
{
#nullable disable
    public class PermissionViewModel
    {
        public string Code { get; set; }
        public string Description { get; set; }
    }
    public class PermissionResponseViewModel
    {
        public Guid Id { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }
    }
    public class PermissionModel
    {
        public Guid Id { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }
    }
    public class AddPermissionToUserModel
    {
        public List<Guid> Ids { get; set; }
    }

    public class RemovePermissionOfUserModel
    {
        public List<Guid> Ids { get; set; }
    }
}
