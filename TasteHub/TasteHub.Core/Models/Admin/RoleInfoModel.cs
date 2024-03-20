namespace TasteHub.Core.Models.Admin
{
    public class RoleInfoModel
    {
        public RoleInfoModel(
            string id,
            string name)
        {
            Id = id; 
            Name = name;
        }
        public string Id { get; set; }
        public string Name { get; set; }
    }
}
