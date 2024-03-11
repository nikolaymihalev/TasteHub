namespace TasteHub.Core.Models
{
    public class RatingInfoModel
    {
        public RatingInfoModel(
            string userId,
            string userUsername,
            int recipeId,
            string recipeTitle,
            double value)
        {
            UserId = userId;
            UserUsername = userUsername;
            RecipeId = recipeId;
            RecipeTitle = recipeTitle;
            Value = value;
        }
        public string UserId { get; set; }
        public string UserUsername { get; set; }
        public int RecipeId { get; set; }
        public string RecipeTitle { get; set; }
        public double Value { get; set; }
    }
}
