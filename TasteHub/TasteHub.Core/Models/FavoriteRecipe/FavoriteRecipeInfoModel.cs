using TasteHub.Core.Models.Recipe;

namespace TasteHub.Core.Models
{
    /// <summary>
    /// Model for information about favorite recipe
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
        /// Recipe model
        /// </summary>
        public RecipeInfoViewModel Recipe { get; set; }
    }
}
