namespace TasteHub.Core.Models.Admin
{
    public class QueryInfoModel
    {
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
        public int Id { get; set; }
        public string UserId { get; set; }
        public string Username { get; set; }
        public string Description { get; set; }
    }
}
