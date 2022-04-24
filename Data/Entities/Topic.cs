#nullable disable

namespace CompatriotsClub.Data
{
    public partial class Topic
    {
        public Topic()
        {
            PostInTopics = new HashSet<PostInTopic>();
        }

        public int Id { get; set; }
        public string Title { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string Description { get; set; }

        public virtual ICollection<PostInTopic> PostInTopics { get; set; }
    }
}
