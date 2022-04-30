using ViewModel.common;

namespace ViewModels.Catalog.Posts
{
#nullable disable
    public class GetPostsRequest : PagingFilter
    {
        public string Keyword { get; set; }
        public DateTime? FromDate { get; set; }
        public DateTime? ToDay { get; set; }
        public Guid? UserId { get; set; }
        public int? ClassId { get; set; }
    }
    public class PostViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateMoodified { get; set; }
        public Guid UserId { get; set; }
    }

    public class PostAddViewModel
    {
        public string Content { get; set; }
        public string Title { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateMoodified { get; set; }
        public Guid UserId { get; set; }
    }
    public class TotalFeed
    {
        public int TotalLikes { get; set; }
        public int TotalLoves { get; set; }
        public int Total { get; set; }
    }
    public class PostResponseViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateMoodified { get; set; }
        public Guid UserId { get; set; }
        public int TotalLikes { get; set; }
        public int TotalLoves { get; set; }
        public int Total { get; set; }
    }
}
