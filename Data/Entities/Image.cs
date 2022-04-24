#nullable disable

namespace CompatriotsClub.Data
{
    public partial class Image
    {


        public int Id { get; set; }
        public string ImagePath { get; set; }
        public DateTime? DateCreated { get; set; }
        public long? FileSize { get; set; }

        public int PostID { get; set; }

        public virtual Post Post { get; set; }
    }
}
