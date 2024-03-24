using TasteHub.Core.Attributes;

namespace TasteHub.Core.Models.Comment
{
    /// <summary>
    /// Model for information about comment
    /// </summary>
    public class CommentInfoModel
    {
        public CommentInfoModel(
            int id,
            string content,
            DateTime creationDate,
            string userId,
            string userUsername,
            int recipeId,
            string recipeTitle)
        {
            Id = id;
            Content = content;
            CreationDate = creationDate;
            UserId = userId;
            UserUsername = userUsername;
            RecipeId = recipeId;
            RecipeTitle = recipeTitle;
        }

        /// <summary>
        /// Comment identifier
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Comment content
        /// </summary>
        public string Content { get; set; }

        /// <summary>
        /// Comment creation date
        /// </summary>
        [DateFormat("dd-MM-yyyy")]
        public DateTime CreationDate { get; set; }

        /// <summary>
        /// User identifier
        /// </summary>
        public string UserId { get; set; }

        /// <summary>
        /// User username
        /// </summary>
        public string UserUsername { get; set; }

        /// <summary>
        /// Recipe identifier
        /// </summary>
        public int RecipeId { get; set; }

        /// <summary>
        /// Recipe title
        /// </summary>
        public string RecipeTitle { get; set; }
    }
}
