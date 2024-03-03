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

        public async Task<Comment> CreateCommentAsync(Comment commentModel)
        {
            await _context.Comments.AddAsync(commentModel);
            await _context.SaveChangesAsync();
            return commentModel;
           
        }

        public async Task<Comment?> DeleteCommentAsync(int id)
        {
            var comment = await _context.Comments.FirstOrDefaultAsync(c=>c.Id==id);
            if(comment==null){
                return null;
            }
            _context.Comments.Remove(comment);
            await _context.SaveChangesAsync();
            return comment;
           
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

        public async Task<Comment?> UpdateCommentAsync(int id, Comment commentModel)
        {
            var previousComment = await _context.Comments.FirstOrDefaultAsync(c=>c.Id==id);
            if(previousComment==null){
                return null;
            }
            previousComment.Title = commentModel.Title;
            previousComment.Content = commentModel.Content;
            await _context.SaveChangesAsync();
            return previousComment;
            
        }
    }
}