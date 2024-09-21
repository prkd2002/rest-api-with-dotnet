using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using api.Dtos.Comment;
using api.Models;

namespace api.Mappers
{
    public static class CommentMappers
    {
        public static CommentDto ToCommentDto(this Comment comment)
        {
            return new CommentDto {
                Id = comment.Id,
                Title = comment.Title,
                Content = comment.Content,
                CreatedOn = comment.CreatedOn,
                StockId = comment.StockId

            };
        }

        public static Comment  toCommentFromCreateDto(this CreateCommentRequest createCommentRequest)
        {
                return new Comment{
                    Title = createCommentRequest.Title,
                    Content = createCommentRequest.Content,
                    CreatedOn = createCommentRequest.CreatedOn,
                    StockId = createCommentRequest.StockId
                };

        }
    }
}