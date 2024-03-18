using TasteHub.Core.Attributes;
using TasteHub.Core.Models.Comment;

namespace TasteHub.Core.Models.Recipe
{
    /// <summary>
    /// Model for recipe information in a database
    /// </summary>
    public class RecipeInfoViewModel
    {
        public RecipeInfoViewModel(
            int id,
            string title,
            string? description,
            string ingredients,
            string instructions,
            DateTime creationDate,
            string image,
            string creatorId,
            string categoryName)
        {
            Id = id;
            Title = title;
            Description = description;
            Ingredients = ingredients;
            Instructions = instructions;
            CreationDate = creationDate;
            Image = image;
            CreatorId = creatorId;
            CategoryName = categoryName;
        }

        /// <summary>
        /// Recipe identifier
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Recipe title
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Recipe description
        /// </summary>
        public string? Description { get; set; }

        /// <summary>
        /// Recipe ingredients
        /// </summary>
        public string Ingredients { get; set; }

        /// <summary>
        /// Instructions for making the recipe
        /// </summary>
        public string Instructions { get; set; }

        /// <summary>
        /// Date of creation
        /// </summary>
        [DateFormat("dd-MM-yyyy")]
        public DateTime CreationDate { get; set; }

        /// <summary>
        /// Image of the food
        /// </summary>
        public string Image { get; set; }

        /// <summary>
        /// Creator identifier
        /// </summary>
        public string CreatorId { get; set; }

        /// <summary>
        /// Category name
        /// </summary>
        public string CategoryName { get; set; }

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