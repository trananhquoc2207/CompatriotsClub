using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;
using ViewModel.common;

namespace ViewModel.Catalogue
{
#nullable disable
    public class GetImagePagingRequest : PagingFilter
    {
        [Required]
        public int AlbumId { get; set; }
        public string ImageName { get; set; }
    }

    public class ImageViewModel
    {
        public int Id { get; set; }
        public string ImagePath { get; set; }
        public DateTime? DateCreated { get; set; }
        public int AlbumId { get; set; }
        public string Name { get; set; }

    }

    public class ListImageViewModel
    {
        public int PostId { get; set; }
        public List<ImageViewModel> Image { get; set; }
        public string Content { get; set; }
        public string Title { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateMoodified { get; set; }
        public Guid UserId { get; set; }
    }

    public class ImageAddViewModel
    {
        public string Name { get; set; }

        [Required]
        public IFormFile File { get; set; }

    }

    public class ImageUpdateViewModel
    {
        public string Name { get; set; }

        public IFormFile File { get; set; }

    }
}
