using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using stockApi.Data;
using stockApi.Interfaces;
using stockApi.Models;

namespace stockApi.Repository
{
    public class CommentRepository:ICommentRepository
    {
        private readonly ApplicationDbContext _context;
        public CommentRepository(ApplicationDbContext context)
        {
            _context = context;
            
        }

        public async Task<List<Comment>> GetAllAsync()
        {
            var comments = await _context.Comments.ToListAsync();
            return comments;
           
        }

        public async Task<Comment?> GetByIdAsync(int id)
        {
            var comment = await _context.Comments.FirstOrDefaultAsync(c=>c.Id==id);
            if(comment==null){
                return null;
            }
            return comment;
        }
    }
}