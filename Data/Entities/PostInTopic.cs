#nullable disable

namespace CompatriotsClub.Data
{
    public partial class PostInTopic
    {
        public int TopicId { get; set; }
        public int PostId { get; set; }

        public virtual Post Post { get; set; }
        public virtual Topic Topic { get; set; }
    }
}
