namespace Data.Entities
{
    public class Image
    {
#nullable disable
        public int Id { get; set; }
        public string ImagePath { get; set; }
        public string Name { get; set; }
        public DateTime? DateCreated { get; set; }
        public long? FileSize { get; set; }

        public int PostId { get; set; }

        public virtual Post Post { get; set; }
    }
}
