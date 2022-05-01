using AutoMapper;
using CompatriotsClub.Data;
using CompatriotsClub.Entities;
using Microsoft.EntityFrameworkCore;
using ViewModel.common;
using ViewModels.Catalog.Posts;

namespace Application.Catalog
{
    public interface ICommentService
    {
        Task<PageResult> GetAllPaging(GetCommentPagingRequest request);
        Task<bool> Delete(int id);
        Task<bool> Update(int id, CommentUpdateViewModel request);
        Task<bool> Add(CommentAddViewModel request);
    }
    public class CommentService : ICommentService
    {
        private readonly CompatriotsClubContext _dbContext;
        private readonly IMapper _mapper;
        public CommentService(CompatriotsClubContext context, IMapper mapper)
        {
            _dbContext = context;
            _mapper = mapper;
        }

        public async Task<bool> Delete(int id)
        {
            var comment = await _dbContext.Comment.FindAsync(id);
            if (comment == null)
                return false;
            _dbContext.Comment.Remove(comment);
            var ss = await _dbContext.SaveChangesAsync();
            if (ss > 0)
                return true;
            return false;
        }

        public async Task<bool> Add(CommentAddViewModel request)
        {
            var comment = _mapper.Map<Comment>(request);
            await _dbContext.Comment.AddAsync(comment);
            var ss = await _dbContext.SaveChangesAsync();
            if (ss > 0)
                return true;
            return false;
        }

        public async Task<PageResult> GetAllPaging(GetCommentPagingRequest request)
        {
            var query = _dbContext.Comment.Where(x => x.PostId == request.PostId);
            if (!string.IsNullOrEmpty(request.Content))
            {
                query = query.Where(x => x.Content.Contains(request.Content));
            }
            var comment = _mapper.Map<List<Comment>, List<CommentViewModel>>(await query.OrderBy(x => x.DateCreated).ToListAsync());
            var commentRes = await query.Skip(request.PageIndex * request.PageSize).Take(request.PageSize).ToListAsync();

            var res = new PageResult();
            res.TotalCounts = comment.Count;
            res.Data = commentRes;
            return res;
        }

        public async Task<bool> Update(int id, CommentUpdateViewModel request)
        {
            var Comment = await _dbContext.Comment.FindAsync(id);

            var ss = await _dbContext.SaveChangesAsync();
            if (ss > 0)
            {
                return true;
            }
            return false;
        }

    }
}
