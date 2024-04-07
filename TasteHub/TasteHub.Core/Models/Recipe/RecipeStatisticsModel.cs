namespace TasteHub.Core.Models.Recipe
{
    /// <summary>
    /// Model for recipe statistics 
    /// </summary>
    public class RecipeStatisticsModel
    {
        /// <summary>
        /// Dictionary of recipe counts by month
        /// </summary>
        public Dictionary<int, int> MonthsCounts { get; set; } = new Dictionary<int, int>();       
    }
}
