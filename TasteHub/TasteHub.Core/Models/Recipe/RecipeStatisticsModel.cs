namespace TasteHub.Core.Models.Recipe
{
    public class RecipeStatisticsModel
    {
        public Dictionary<int, int> MonthsCounts { get; set; } = new Dictionary<int, int>();       
    }
}
