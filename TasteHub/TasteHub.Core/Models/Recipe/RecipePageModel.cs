using TasteHub.Core.Models.Comment;

namespace TasteHub.Core.Models.Recipe
{
    public class RecipePageModel
    {
        /// <summary>
        /// Property for checking if the recipe is in collection of favorite recipes in the user
        /// </summary>
        public bool IsInUserFavoriteCollection { get; set; }

        /// <summary>
        /// Property for last comment about recipe
        /// </summary>
        public CommentInfoModel LastComment { get; set; }

        /// <summary>
        /// Property for average rating 
        /// </summary>
        public double AverageRating { get; set; }
    }
}
