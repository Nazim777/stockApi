using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using stockApi.Models;

namespace stockApi.Interfaces
{
    public interface ICommentRepository
    {
        Task<List<Comment>> GetAllAsync();
        Task<Comment?>GetByIdAsync(int id);
        Task<Comment>CreateCommentAsync(Comment commentModel);
        Task<Comment?>UpdateCommentAsync( int id,Comment commentModel);
        Task<Comment?>DeleteCommentAsync(int id);
    }
}