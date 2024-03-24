namespace TasteHub.Core.Models.Category
{
    /// <summary>
    /// Model for information about category
    /// </summary>
    public class CategoryInfoViewModel
    {
        public CategoryInfoViewModel(
            int id,
            string name)
        {
            Id = id;
            Name = name;
        }

        /// <summary>
        /// Category identifier
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Category name
        /// </summary>
        public string Name { get; set; }
    }
}
