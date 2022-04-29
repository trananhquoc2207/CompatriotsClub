using CompatriotsClub.Data;

namespace Data.Entities
{
#nullable disable
    public class Comment
    {
        public string Content { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateMoodified { get; set; }
        public virtual Post Post { get; set; }
        public virtual AppUser User { get; set; }
        public int Id { get; set; }
        public int PostId { get; set; }
        public Guid UserId { get; set; }
    }
}
