namespace TasteHub.Core.Models.Recipe
{
    /// <summary>
    /// Model for query 
    /// </summary>
    public class RecipeQueryModel
    {
        /// <summary>
        /// Collection of recipes
        /// </summary>
        public IEnumerable<RecipeInfoViewModel> Recipes { get; set; } = new List<RecipeInfoViewModel>();

        /// <summary>
        /// Date filter
        /// </summary>
        public string? Sorting { get; set; }

        /// <summary>
        /// Category name filter
        /// </summary>
        public string? Category { get; set; }

        /// <summary>
        /// Count for pages
        /// </summary>
        public double PagesCount { get; set; }

        /// <summary>
        /// Current page
        /// </summary>
        public int CurrentPage { get; set; }
    }
}
