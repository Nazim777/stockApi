using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using stockApi.Dtos.Comment;
using stockApi.Models;

namespace stockApi.Mapper
{
    public static class CommentMapper
    {
        public static CommentDto mapToCommentDto(this Comment commentModel){
            return new CommentDto{
                Id = commentModel.Id,
                Title=commentModel.Title,
                Content = commentModel.Content,
                CreatedOn = commentModel.CreatedOn,
                StockId = commentModel.StockId

            };
        }

       
        
    }
}