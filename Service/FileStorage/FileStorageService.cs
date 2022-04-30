using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using System.Net;

namespace Service.Common
{
    public interface IFileStorageService
    {
        string GetPathOfFile(string directory, string fileName);

        Task SaveFileAsync(Stream binaryStream, string directory, string fileName);

        void SaveFileByUrl(string url, string directory, string fileName);

        Task SaveFileByUrlAsync(string url, string directory, string fileName);

        Task DeleteFileAsync(string directory, string fileName);

        string GetPathOfAvatar(string fileName);

        Task SaveAvatar(Stream binaryStream, string fileName);

        void SaveAvatarByUrl(string url, string fileName);

        Task SaveAvatarByUrlAsync(string url, string fileName);

        Task DeleteAvatar(string fileName);

        string GetPathOfThumbnail(string fileName);

        Task SaveThumbnail(Stream binaryStream, string fileName);

        void SaveThumbnailByUrl(string url, string fileName);

        Task SaveThumbnailByUrlAsync(string url, string fileName);

        Task DeleteThumbnail(string fileName);

        string GetPathOfRecognizationImage(string fileName);

        Task SaveRecognizationImage(Stream binaryStream, string fileName);

        void SaveRecognizationImageByUrl(string url, string fileName);

        Task SaveRecognizationImageByUrlAsync(string url, string fileName);

        Task DeleteRecognizationImage(string fileName);
    }

    public class FileStorageService : IFileStorageService
    {
        private readonly string _avatarFolder;
        private readonly string _thumbnailFolder;
        private readonly string _recognizationFolder;

        private const string IMAGE_FOLDER_NAME = "images";
        private const string AVATAR_FOLDER_NAME = $"{IMAGE_FOLDER_NAME}/avatars";
        private const string THUMBNAIL_FOLDER_NAME = $"{IMAGE_FOLDER_NAME}/thumbnails";
        private const string RECOGNIZATION_FOLDER_NAME = $"{IMAGE_FOLDER_NAME}/recognizations";

        public FileStorageService(IWebHostEnvironment webHostEnvironment)
        {
            _avatarFolder = Path.Combine(webHostEnvironment.WebRootPath, AVATAR_FOLDER_NAME);
            _thumbnailFolder = Path.Combine(webHostEnvironment.WebRootPath, THUMBNAIL_FOLDER_NAME);
            _recognizationFolder = Path.Combine(webHostEnvironment.WebRootPath, RECOGNIZATION_FOLDER_NAME);

            if (!Directory.Exists(_avatarFolder))
                Directory.CreateDirectory(_avatarFolder);

            if (!Directory.Exists(_thumbnailFolder))
                Directory.CreateDirectory(_thumbnailFolder);

            if (!Directory.Exists(_recognizationFolder))
                Directory.CreateDirectory(_recognizationFolder);
        }

        public string GetPathOfFile(string directory, string fileName) => $"{directory}/{fileName}";

        public async Task SaveFileAsync(Stream binaryStream, string directory, string fileName)
        {
            var filePath = Path.Combine(directory, fileName);
            using var handler = new FileStream(filePath, FileMode.Create);
            await binaryStream.CopyToAsync(handler);
        }

        public void SaveFileByUrl(string url, string directory, string fileName)
        {
            using (WebClient client = new WebClient())
            {
                Uri address = new Uri(url);
                var filePath = Path.Combine(directory, fileName);
                client.DownloadFile(address, filePath);
            }
        }

        public async Task SaveFileByUrlAsync(string url, string directory, string fileName)
        {
            using (WebClient client = new WebClient())
            {
                Uri address = new Uri(url);
                var filePath = Path.Combine(directory, fileName);
                await client.DownloadFileTaskAsync(address, filePath);
            }
        }

        public async Task DeleteFileAsync(string directory, string fileName)
        {
            var filePath = Path.Combine(directory, fileName);
            if (File.Exists(filePath))
                await Task.Run(() => File.Delete(filePath));
        }

        public string GetPathOfAvatar(string fileName) => GetPathOfFile(AVATAR_FOLDER_NAME, fileName);

        public async Task SaveAvatar(Stream binaryStream, string fileName) => await SaveFileAsync(binaryStream, _avatarFolder, fileName);

        public void SaveAvatarByUrl(string url, string fileName) => SaveFileByUrl(url, _avatarFolder, fileName);

        public async Task SaveAvatarByUrlAsync(string url, string fileName) => await SaveFileByUrlAsync(url, _avatarFolder, fileName);

        public async Task DeleteAvatar(string fileName) => await DeleteFileAsync(_avatarFolder, fileName);

        public string GetPathOfThumbnail(string fileName) => GetPathOfFile(THUMBNAIL_FOLDER_NAME, fileName);

        public async Task SaveThumbnail(Stream binaryStream, string fileName) => await SaveFileAsync(binaryStream, _thumbnailFolder, fileName);

        public void SaveThumbnailByUrl(string url, string fileName) => SaveFileByUrl(url, _thumbnailFolder, fileName);

        public async Task SaveThumbnailByUrlAsync(string url, string fileName) => await SaveFileByUrlAsync(url, _thumbnailFolder, fileName);

        public async Task DeleteThumbnail(string fileName) => await DeleteFileAsync(_recognizationFolder, fileName);

        public string GetPathOfRecognizationImage(string fileName) => GetPathOfFile(RECOGNIZATION_FOLDER_NAME, fileName);

        public async Task SaveRecognizationImage(Stream binaryStream, string fileName) => await SaveFileAsync(binaryStream, _recognizationFolder, fileName);

        public void SaveRecognizationImageByUrl(string url, string fileName) => SaveFileByUrl(url, _recognizationFolder, fileName);

        public async Task SaveRecognizationImageByUrlAsync(string url, string fileName) => await SaveFileByUrlAsync(url, _recognizationFolder, fileName);

        public async Task DeleteRecognizationImage(string fileName) => await DeleteFileAsync(_recognizationFolder, fileName);

    }
    public class MyHttpContext
    {
        private static IHttpContextAccessor m_httpContextAccessor;

        public static HttpContext Current => m_httpContextAccessor.HttpContext;

        public static string AppBaseUrl => $"{Current.Request.Scheme}://{Current.Request.Host}{Current.Request.PathBase}";

        internal static void Configure(IHttpContextAccessor contextAccessor)
        {
            m_httpContextAccessor = contextAccessor;
        }


    }
    public static class HttpContextExtensions
    {
        public static IApplicationBuilder UseHttpContext(this IApplicationBuilder app)
        {
            MyHttpContext.Configure(app.ApplicationServices.GetRequiredService<IHttpContextAccessor>());
            return app;
        }
    }
}