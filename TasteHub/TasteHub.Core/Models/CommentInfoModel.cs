﻿namespace TasteHub.Core.Models
{
    public class CommentInfoModel
    {
        public CommentInfoModel(
            int id,
            string content,
            DateTime creationDate,
            string userId,
            string userUsername,
            int recipeId)
        {
            Id = id;
            Content = content;
            CreationDate = creationDate;
            UserId = userId;
            UserUsername = userUsername;
            RecipeId = recipeId;
        }

        public int Id { get; set; }
        public string Content { get; set; }
        public DateTime CreationDate { get; set; }
        public string UserId { get; set; }
        public string UserUsername { get; set; }
        public int RecipeId { get; set; }
        
    }
}
