namespace TasteHub.Core.Models.User
{
    public class UserViewModel
    {
        public UserViewModel(
            string id,
            string username,
            int recipesCount)
        {
            Id = id;
            Username = username;
            RecipesCount = recipesCount;
        }
        public string Id { get; set; }
        public string Username { get; set; }
        public int RecipesCount { get; set; }
    }
}
