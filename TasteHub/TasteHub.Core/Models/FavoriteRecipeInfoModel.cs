namespace TasteHub.Core.Models
{
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
        public string CreatorId { get; set; }
        public string CreatorUsername { get; set; }
        public int RecipeId { get; set; }
    }
}
