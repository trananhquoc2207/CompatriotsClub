using System.ComponentModel.DataAnnotations;
using ViewModel.common;

namespace ViewModels.Catalog.Posts
{
#nullable disable
    public class CommentViewModel
    {
        public string Content { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateMoodified { get; set; }
        public int Id { get; set; }
        public int PostId { get; set; }
        public Guid UserId { get; set; }
    }
    public class CommentUpdateViewModel
    {
    }

    public class CommentAddViewModel
    {
        [Required]
        public int PostId { get; set; }
        [Required]
        public Guid UserId { get; set; }
        public string Content { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateMoodified { get; set; }

    }
    public class GetCommentPagingRequest : PagingFilter
    {
        [Required]
        public int PostId { get; set; }
        public string Content { get; set; }
    }
}
