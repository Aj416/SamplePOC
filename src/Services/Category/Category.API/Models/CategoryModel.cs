namespace Category.API.Models
{
    public class CategoryModel
    {
        public string Name { get; set; }
        public string Description { get; set; }

        public CategoryModel(string name, string description)
        {
            Name = name;
            Description = description;
        }
    }
}
