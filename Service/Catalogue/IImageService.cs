using AutoMapper;
using CompatriotsClub.Data;
using CompatriotsClub.Entities;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Service.Common;
using System.Net.Http.Headers;
using ViewModel.Catalogue;
using ViewModel.common;

namespace Service.Catalogue
{
    public interface IImageService
    {
        Task<PageResult> GetAllPaging(GetImagePagingRequest request);
        Task<bool> Delete(int id);
        Task<bool> Update(int id, ImageUpdateViewModel request);
    }
    public class ImageService : IImageService
    {
        private readonly CompatriotsClubContext _dbContext;
        private readonly IMapper _mapper;
        private readonly IFileStorageService _storageService;
        private readonly string _thumbnailFolder;
        private const string IMAGE_FOLDER_NAME = "images";
        private const string THUMBNAIL_FOLDER_NAME = $"{IMAGE_FOLDER_NAME}/thumbnails";
        public ImageService(CompatriotsClubContext context, IMapper mapper, IFileStorageService storageService, IWebHostEnvironment webHostEnvironment)
        {
            _thumbnailFolder = Path.Combine(webHostEnvironment.WebRootPath, THUMBNAIL_FOLDER_NAME);
            _dbContext = context;
            _mapper = mapper;
            _storageService = storageService;
        }

        public async Task<bool> Delete(int id)
        {
            var image = await _dbContext.Images.FindAsync(id);
            if (image == null)
                return false;
            await _storageService.DeleteFileAsync(_thumbnailFolder, image.ImagePath);
            _dbContext.Images.Remove(image);
            var ss = await _dbContext.SaveChangesAsync();
            if (ss > 0)
                return true;
            return false;
        }

        public async Task<PageResult> GetAllPaging(GetImagePagingRequest request)
        {
            var query = _dbContext.Images.Where(x => x.PostId == request.AlbumId);
            if (!string.IsNullOrEmpty(request.ImageName))
            {
                query = query.Where(x => x.Name.Contains(request.ImageName));
            }
            var images = _mapper.Map<List<Image>, List<ImageViewModel>>(await query.ToListAsync());
            var res = new PageResult();
            res.TotalCounts = images.Count;
            res.Data = images;
            return res;
        }

        public async Task<bool> Update(int id, ImageUpdateViewModel request)
        {
            var image = await _dbContext.Images.FindAsync(id);
            if (image == null)
                return false;
            image.Name = request.Name;

            if (request.File != null)
            {
                await _storageService.DeleteFileAsync(_thumbnailFolder, image.ImagePath);
                image.FileSize = request.File.Length;
                image.DateCreated = DateTime.Now;
                image.ImagePath = await this.SaveFile(request.File);
            };
            var ss = await _dbContext.SaveChangesAsync();
            if (ss > 0)
            {
                return true;
            }
            return false;
        }
        private async Task<string> SaveFile(IFormFile file)
        {
            var originalFileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
            var fileName = $"{Guid.NewGuid()}{Path.GetExtension(originalFileName)}";
            await _storageService.SaveFileAsync(file.OpenReadStream(), _thumbnailFolder, fileName);
            return fileName;
        }
    }
}
