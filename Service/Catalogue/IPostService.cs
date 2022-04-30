using AutoMapper;
using CompatriotsClub.Data;
using CompatriotsClub.Entities;
using Data.Enum;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Service.Base;
using Service.Common;
using System.Net.Http.Headers;
using ViewModel.Catalogue;
using ViewModel.common;
using ViewModels.Catalog.Posts;
using Z.EntityFramework.Plus;

namespace Service.Catalogue
{
#nullable disable
    public interface IPostService : IBaseService<Post>
    {
        Task<PageResult> GetPagedResult(GetPostsRequest filter);
        Task<bool> AddImage(ImageAddViewModel request, int AlbumId);
        Task<PageResult> GetImage(int albumId);
        Task<ResultModel> Feel(int id, ActionPost action, Guid userId);
        Task<PostResponseViewModel> GetByPostId(int postId);
    }
    public class PostService : BaseService<Post>, IPostService
    {
        private readonly IFileStorageService _storageService;
        private readonly IMapper _mapper;
        public PostService(CompatriotsClubContext dbContext, IFileStorageService storageService, IMapper mapper) : base(dbContext)
        {
            _storageService = storageService;
            _mapper = mapper;
        }

        public async Task<PageResult> GetPagedResult(GetPostsRequest filter)
        {
            var result = new PageResult()
            {
                TotalCounts = 0,
                Data = new List<PostResponseViewModel>()
            };

            var query = _dbContext.Posts
                            .Where(_ => string.IsNullOrEmpty(filter.Keyword) || _.Title.ToLower().Contains(filter.Keyword.ToLower()));
            var users = await query.Skip(filter.PageIndex * filter.PageSize).Take(filter.PageSize).ToListAsync();

            var userModels = new List<PostResponseViewModel>();
            foreach (var user in users)
            {
                var userModel = _mapper.Map<Post, PostResponseViewModel>(user);

                userModels.Add(userModel);
            }

            result.TotalCounts = await query.CountAsync();
            result.Data = userModels;

            return result;
        }

        public async Task<bool> AddImage(ImageAddViewModel request, int AlbumId)
        {
            if (request != null)
            {
                var image = new Image()
                {
                    Name = request.Name,
                    PostId = AlbumId,
                    DateCreated = DateTime.Now,
                    FileSize = request.File.Length,
                    ImagePath = await this.SaveFile(request.File),

                };
                _dbContext.Images.Add(image);
                var ss = await _dbContext.SaveChangesAsync();
                if (ss > 0)
                {
                    return true;
                }
            }
            return false;
        }

        //public IEnumerable<PostResponseViewModel> Fill(GetPostsRequest request, out Meta meta)
        //{
        //    var query = GetIQueryable();
        //    if (request.FromDate.HasValue || request.ToDay.HasValue)
        //    {
        //        query = query.Where(x => x.DateCreated < request.ToDay.Value.Date.AddDays(1) && x.DateCreated >= request.FromDate.Value.Date);
        //    }
        //    if (!string.IsNullOrEmpty(request.Title))
        //    {
        //        query = query.Where(x => x.Title.Contains(request.Title));
        //    }
        //    if (request.ClassId.HasValue)
        //    {
        //        query = query.Where(x => x.ClassId == request.ClassId.Value);
        //    }

        //    if (request.UserId.HasValue)
        //    {
        //        query = query.Where(x => x.UserId == request.UserId.Value);
        //    }
        //    meta = Meta.ProcessAndCreate(query.Count(), request.PageSize, request.PageIndex);
        //    var result = query.Select(x => new PostResponseViewModel()
        //    {
        //        ClassId = x.ClassId,
        //        Content = x.Content,
        //        DateCreated = x.DateCreated,
        //        Title = x.Title,
        //        DateMoodified = x.DateMoodified,
        //        Id = x.Id,
        //        AppUser = _mapper.Map<UserViewModel>(_dbContext.Users.FirstOrDefault(y => y.Id == x.UserId)),
        //        UserId = x.UserId,
        //        TotalLikes = _dbContext.Feel.Where(y => y.Action == ActionPost.Like && y.PostId == x.Id).Count(),
        //        TotalLoves = _dbContext.Feel.Where(y => y.Action == ActionPost.Love && y.PostId == x.Id).Count(),
        //        Total = _dbContext.Feel.Where(y => y.Action == ActionPost.Like && y.PostId == x.Id).Count() + _dbContext.Feel.Where(y => y.Action == ActionPost.Love && y.PostId == x.Id).Count()
        //    });

        //    return result.ToPagedList(meta.page_number, meta.page_size);
        //}

        private TotalFeed Total(int id)
        {
            var totalLike = _dbContext.Feel.Where(x => x.Action == ActionPost.Like).Count();
            var totalLove = _dbContext.Feel.Where(x => x.Action == ActionPost.Like).Count();

            var total = totalLike + totalLove;
            var res = new TotalFeed() { Total = total, TotalLikes = totalLike, TotalLoves = totalLove };
            return res;
        }
        public async Task<PageResult> GetImage(int albumId)
        {
            var query = _dbContext.Images.Where(x => x.PostId == albumId);
            var images = _mapper.Map<List<Image>, List<ImageViewModel>>(await query.ToListAsync());
            foreach (var image in images)
            {
                if (!string.IsNullOrEmpty(image.ImagePath))
                {

                    image.ImagePath = MyHttpContext.AppBaseUrl + "/" + _storageService.GetPathOfThumbnail(image.ImagePath);
                }
            }
            var album = await _dbContext.Posts.FindAsync(albumId);
            var listImage = new ListImageViewModel()
            {
                PostId = albumId,
                DateCreated = album.DateCreated,
                Title = album.Title,
                Image = images,
                Content = album.Content,
                UserId = album.UserId,
                DateMoodified = album.DateMoodified,
            };
            var res = new PageResult();
            res.TotalCounts = images.Count;
            res.Data = listImage;
            return res;
        }

        private async Task<string> SaveFile(IFormFile file)
        {
            var originalFileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
            var fileName = $"{Guid.NewGuid()}{Path.GetExtension(originalFileName)}";
            await _storageService.SaveThumbnail(file.OpenReadStream(), fileName);
            return fileName;
        }

        public async Task<ResultModel> Feel(int id, ActionPost action, Guid userId)
        {
            var res = new ResultModel();
            var post = await _dbContext.Posts.FindAsync(id);
            if (post == null)
            {
                res.ErrorMessages = "Did not find the post";
                return res;
            }
            var feeda = await _dbContext.Feel.FirstOrDefaultAsync
                (x => x.PostId == id && x.UserId == userId);
            if (feeda != null)
            {
                if (feeda.Action == action)
                {
                    _dbContext.Feel.Remove(feeda);
                    _dbContext.SaveChanges();
                    res.Succeed = true;
                    res.Data = userId;
                    return res;
                }
                else
                {
                    feeda.Action = action;
                    _dbContext.Feel.Update(feeda);
                    _dbContext.SaveChanges();
                    res.Succeed = true;
                    res.Data = userId;
                }
            }

            feeda = new Feel() { Action = action, PostId = id, UserId = userId };
            await _dbContext.Feel.AddAsync(feeda);
            var ss = await _dbContext.SaveChangesAsync();
            QueryCacheManager.ExpireTag(typeof(Feel).FullName);
            if (ss == 0)
            {
                res.ErrorMessages = "No changes";
                return res;
            }
            res.Succeed = true;
            res.Data = userId;
            return res;
        }

        public async Task<PostResponseViewModel> GetByPostId(int postId)
        {
            var post = await _dbContext.Posts.FindAsync(postId);
            var res = _mapper.Map<PostResponseViewModel>(post);
            var total = Total(postId);
            res.TotalLikes = total.TotalLikes;
            res.TotalLoves = total.TotalLoves;
            res.Total = total.Total;
            return res;
        }
    }
}
