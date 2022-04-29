using CompatriotsClub.Data;
using Data.Enum;

namespace Data.Entities
{
#nullable disable
    public class Post
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public string Title { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateMoodified { get; set; }
        public Guid UserId { get; set; }
        public int? ClassId { get; set; }
        public virtual AppUser AppUser { get; set; }
        public virtual List<Image> Images { get; set; }
        public virtual List<Comment> Conments { get; set; }
        public virtual List<Feel> Feel { get; set; }
    }

    public class Feel
    {
        public int Id { get; set; }
        public int PostId { get; set; }
        public virtual Post Post { get; set; }
        public virtual AppUser User { get; set; }
        public Guid UserId { get; set; }
        public ActionPost Action { get; set; }

    }
}