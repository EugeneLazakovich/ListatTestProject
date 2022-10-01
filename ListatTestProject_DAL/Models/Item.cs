namespace ListatTestProject_DAL.Models
{
    public class Item : BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Metadata { get; set; }
    }
}
