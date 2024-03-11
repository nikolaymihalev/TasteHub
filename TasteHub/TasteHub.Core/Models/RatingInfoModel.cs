namespace TasteHub.Core.Models
{
    public class RatingInfoModel
    {
        public RatingInfoModel(
            string userId,
            string userUsername,
            int recipeId,
            string recipeTitle)
        {
            UserId = userId;
            UserUsername = userUsername;
            RecipeId = recipeId;
            RecipeTitle = recipeTitle;
        }
        public string UserId { get; set; }
        public string UserUsername { get; set; }
        public int RecipeId { get; set; }
        public string RecipeTitle { get; set; }
    }
}
