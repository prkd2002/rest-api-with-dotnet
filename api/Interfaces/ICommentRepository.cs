using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos.Comment;
using api.Models;

namespace api.Interfaces
{
    public interface ICommentRepository
    {
        Task<List<Comment>> GetAllAsync();
        Task<Comment?> GetByIdAsync(int id);

        Task<Comment?> UpdateCommentAsync(int id, UpdateCommentRequest updateCommentRequest);

        Task<Comment?> RemoveCommentAsync(int id);

        Task<Comment> CreateCommentAsync(Comment comment);


        
    }
}