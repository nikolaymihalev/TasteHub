namespace TasteHub.Core.Models.Admin
{
    /// <summary>
    /// Model for information about role
    /// </summary>
    public class RoleInfoModel
    {
        public RoleInfoModel(
            string id,
            string name)
        {
            Id = id; 
            Name = name;
        }

        /// <summary>
        /// Role information
        /// </summary>
        public string Id { get; set; }
        
        /// <summary>
        /// Role name
        /// </summary>
        public string Name { get; set; }
    }
}
