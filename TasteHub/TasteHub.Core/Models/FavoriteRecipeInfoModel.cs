namespace TasteHub.Core.Models
{
    /// <summary>
    /// Model for favorite recipe information in a database
    /// </summary>
    public class FavoriteRecipeInfoModel
    {
        public FavoriteRecipeInfoModel(
            string userId,
            int recipeId,
            RecipeInfoViewModel recipe)
        {
            UserId = userId;
            RecipeId = recipeId;
            Recipe = recipe;
        }
        /// <summary>
        /// Creator identifier
        /// </summary>
        public string UserId { get; set; }

        /// <summary>
        /// Recipe identifier
        /// </summary>
        public int RecipeId { get; set; }

        /// <summary>
        /// Recipe
        /// </summary>
        public RecipeInfoViewModel Recipe { get; set; }
    }
}
