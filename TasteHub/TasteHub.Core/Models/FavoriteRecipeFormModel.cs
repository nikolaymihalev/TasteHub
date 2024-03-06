namespace TasteHub.Core.Models
{
    public class FavoriteRecipeFormModel
    {
        public FavoriteRecipeFormModel(
            string creatorId,
            int recipeId)
        {
            CreatorId = creatorId;
            RecipeId = recipeId;
        }
        public string CreatorId { get; set; }
        public int RecipeId { get; set; }
    }
}
