using FinShark.DTOs.Comment;
using FinShark.Models;

namespace FinShark.Mappers;

public static class CommentMappers
{
    public static CommentDto ToCommentDto(this Comment commentModel)
    {
        return new CommentDto
        {
            Id = commentModel.Id,
            Content = commentModel.Content,
            Title = commentModel.Title,
            CreatedOn = commentModel.CreatedOn,
            StockId = commentModel.StockId
        };
    }
}