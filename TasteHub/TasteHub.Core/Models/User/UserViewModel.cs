namespace TasteHub.Core.Models.User
{
    public class UserViewModel
    {
        public UserViewModel(
            string id,
            string username)
        {
            Id = id;
            Username = username;
        }
        public string Id { get; set; }
        public string Username { get; set; }
        public int RecipesCount { get; set; }
    }
}
