namespace TasteHub.Core.Models
{
    /// <summary>
    /// Model for favorite recipe information in a database
    /// </summary>
    public class FavoriteRecipeInfoModel
    {
        public FavoriteRecipeInfoModel(
            string creatorId,
            string creatorUsername,
            int recipeId)
        {
            CreatorId = creatorId;
            CreatorUsername = creatorUsername;
            RecipeId = recipeId;
        }
        /// <summary>
        /// Creator identifier
        /// </summary>
        public string CreatorId { get; set; }

        /// <summary>
        /// Creator username
        /// </summary>
        public string CreatorUsername { get; set; }

        /// <summary>
        /// Recipe identifier
        /// </summary>
        public int RecipeId { get; set; }
    }
}
