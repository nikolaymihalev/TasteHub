namespace TasteHub.Core.Models
{
    public class RatingInfoModel
    {
        public RatingInfoModel(
            int id,
            string userId,
            string userUsername,
            int recipeId,
            string recipeTitle)
        {
            Id = id;
            UserId = userId;
            UserUsername = userUsername;
            RecipeId = recipeId;
            RecipeTitle = recipeTitle;
        }
        public int Id { get; set; }
        public string UserId { get; set; }
        public string UserUsername { get; set; }
        public int RecipeId { get; set; }
        public string RecipeTitle { get; set; }
    }
}
