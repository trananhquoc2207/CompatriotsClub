namespace ViewModel.Catalogue
{
#nullable disable
    public class GroupViewModel
    {
        public string Name { get; set; }
        public string Region { get; set; }
        public string Description { get; set; }
        public int? IdMember { get; set; }
    }
    public class GroupResponseViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Region { get; set; }
        public string Description { get; set; }
        public int? IdMember { get; set; }
    }
}
