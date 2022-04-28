﻿using System.ComponentModel.DataAnnotations;

#nullable disable

namespace CompatriotsClub.Data
{
    public partial class Post
    {
        public Post()
        {
            Images = new HashSet<Image>();
            PostInTopics = new HashSet<PostInTopic>();
        }
        [Key]
        public int Id { get; set; }
        public string Title { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public Guid? AuthorId { get; set; }
        public string Content { get; set; }

        public virtual ICollection<Image> Images { get; set; }
        public virtual ICollection<PostInTopic> PostInTopics { get; set; }
    }
}
