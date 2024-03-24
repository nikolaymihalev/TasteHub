namespace TasteHub.Core.Models.User
{
    /// <summary>
    /// Model for information about user
    /// </summary>
    public class UserViewModel
    {
        public UserViewModel(
            string id,
            string username)
        {
            Id = id;
            Username = username;
        }

        /// <summary>
        /// User identifier
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// User username
        /// </summary>
        public string Username { get; set; }

        /// <summary>
        /// User recipes count
        /// </summary>
        public int RecipesCount { get; set; }
    }
}
