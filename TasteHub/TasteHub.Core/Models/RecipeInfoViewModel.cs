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
            string creatorId,
            int categoryId)
        {
            Id = id; 
            Title = title; 
            Description = description;
            Ingredients = ingredients;
            Instructions = instructions;
            CreationDate = creationDate;
            Image = image;
            CreatorId = creatorId;
            CategoryId = categoryId;
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
        /// Creator identifier
        /// </summary>
        public string CreatorId { get; set; } 

        /// <summary>
        /// Category identifier
        /// </summary>
        public int CategoryId { get; set; }
    }
}
