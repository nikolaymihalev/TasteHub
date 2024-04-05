using TasteHub.Core.Models.Comment;
using TasteHub.Infrastructure.Constants;

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

        public string? Sorting { get; set; }
        public int MaxPerPage { get; set; } = ValidationConstants.MaxRecipesPerPage;
        public int CurrentPage { get; set; }
    }
}
