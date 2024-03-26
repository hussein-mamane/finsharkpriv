﻿namespace FinShark.DTOs.Comment;

public class CommentDto
{
    public int Id { get; set; }
    public string? Content { get; set; }
    public string Title { get; set; } = string.Empty;
    public DateTime CreatedOn { get; set; } = DateTime.Now;
    public int? StockId { get; set; }
}