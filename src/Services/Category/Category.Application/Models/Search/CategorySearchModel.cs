namespace Category.Application.Models.Search
{
    public class CategorySearchModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        ///<summary>
        ///This is just a work-around for Elasticsearch to sort by name, currently the current settings although set perfectly, still doesn't work.
        ///TODO: Just remove this property, when this question is answered https://discuss.elastic.co/t/case-insensitive-sort-doesnt-work/143192
        ///</summary>
        public string NameSortToken => Name.Replace(" ", "").ToLower();

        public string Description { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }
    }
}
