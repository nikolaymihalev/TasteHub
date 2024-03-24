namespace TasteHub.Core.Models.Admin
{
    public class QueryInfoModel
    {
        /// <summary>
        /// Model for information about query
        /// </summary>
        public QueryInfoModel(
            int id,
            string userId,
            string username,
            string description)
        {
            Id = id;
            UserId = userId;    
            Username = username;
            Description = description;
        }

        /// <summary>
        /// Query identifier
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// User identifier
        /// </summary>
        public string UserId { get; set; }

        /// <summary>
        /// User username
        /// </summary>
        public string Username { get; set; }

        /// <summary>
        /// Query description
        /// </summary>
        public string Description { get; set; }
    }
}
