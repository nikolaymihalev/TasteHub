namespace TasteHub.Core.Models
{
    /// <summary>
    /// Model for favorite recipe information in a database
    /// </summary>
    public class FavoriteRecipeInfoModel
    {
        public FavoriteRecipeInfoModel(
            string creatorId,
            int recipeId,
            RecipeInfoViewModel recipe)
        {
            CreatorId = creatorId;
            RecipeId = recipeId;
            Recipe = recipe;
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

        /// <summary>
        /// Recipe
        /// </summary>
        public RecipeInfoViewModel Recipe { get; set; }
    }
}
