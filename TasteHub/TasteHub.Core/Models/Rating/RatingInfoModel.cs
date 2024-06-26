﻿namespace TasteHub.Core.Models.Rating
{
    /// <summary>
    /// Model for information about rating
    /// </summary>
    public class RatingInfoModel
    {
        public RatingInfoModel(
            int id,
            string userId,
            string userUsername,
            int recipeId,
            string recipeTitle,
            double value)
        {
            Id = id;
            UserId = userId;
            UserUsername = userUsername;
            RecipeId = recipeId;
            RecipeTitle = recipeTitle;
            Value = value;
        }

        /// <summary>
        /// Rating identifier
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// User identifier
        /// </summary>
        public string UserId { get; set; }

        /// <summary>
        /// User username
        /// </summary>
        public string UserUsername { get; set; }

        /// <summary>
        /// Recipe identifier
        /// </summary>
        public int RecipeId { get; set; }

        /// <summary>
        /// Recipe title
        /// </summary>
        public string RecipeTitle { get; set; }

        /// <summary>
        /// Rating value
        /// </summary>
        public double Value { get; set; }
    }
}
