namespace TasteHub.Core.Models
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
            string creationDate,
            byte[] image,
            string creatorUsername,
            string categoryName)
        {
            Id = id; 
            Title = title; 
            Description = description;
            Ingredients = ingredients;
            Instructions = instructions;
            CreationDate = creationDate;
            Image = image;
            CreatorUsername = creatorUsername;
            CategoryName = CategoryName;
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
        public string CreationDate { get; set; }

        /// <summary>
        /// Image of the food
        /// </summary>
        public byte[] Image { get; set; }
        
        /// <summary>
        /// Creator username
        /// </summary>
        public string CreatorUsername { get; set; } 

        /// <summary>
        /// Category name
        /// </summary>
        public string CategoryName { get; set; }
    }
}