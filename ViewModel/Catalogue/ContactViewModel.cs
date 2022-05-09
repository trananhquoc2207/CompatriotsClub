using ViewModel.common;

namespace ViewModel.Catalogue
{
#nullable disable

    public class ContactViewModel
    {
        public string Name { get; set; }
        public string Description { get; set; }
    }
    public class ContactResponseViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
    public class ContactMembersRequest
    {
        public int MemberId { get; set; }
        public int PositionId { get; set; }
    }
    public class ContactFilter : PagingFilter
    {
        public string Keyword { get; set; }
    }
}
