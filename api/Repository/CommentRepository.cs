using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.Dtos.Comment;
using api.Interfaces;
using api.Models;
using Microsoft.EntityFrameworkCore;

namespace api.Repository
{
    public class CommentRepository : ICommentRepository
    {

        private readonly ApplicationDBContext _context;
        public CommentRepository(ApplicationDBContext context)
        {
            _context = context;
        }

        public async Task<Comment> CreateCommentAsync(Comment comment)
        {
            await _context.Comments.AddAsync(comment);
            await _context.SaveChangesAsync();
            return comment;
        }

        public async Task<List<Comment>> GetAllAsync()
        {
            var Comments = await _context.Comments.ToListAsync();
            return Comments;
        }

        public async Task<Comment?> GetByIdAsync(int id)
        {
            var commentModel = await _context.Comments.FirstOrDefaultAsync(com => com.Id == id);
            if(commentModel == null){
                return null;
            }
            return commentModel;
        }

        public async Task<Comment?> RemoveCommentAsync(int id)
        {
            var commentModel = await _context.Comments.FirstOrDefaultAsync(com => com.Id == id);
            if(commentModel == null){
                return null;
            }
            _context.Comments.Remove(commentModel);
            return commentModel;
        }

        public async Task<Comment?> UpdateCommentAsync(int id, UpdateCommentRequest updateCommentRequest)
        {
            var commentModel = await _context.Comments.FirstOrDefaultAsync(com => com.Id == id);
            if(commentModel == null){
                return null;
            }
            commentModel.Content = updateCommentRequest.Content;
            commentModel.Title = updateCommentRequest.Title;
            commentModel.CreatedOn = updateCommentRequest.CreatedOn;
            await _context.SaveChangesAsync();
            return commentModel;
        }

    }
}