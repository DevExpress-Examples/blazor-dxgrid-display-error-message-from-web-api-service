namespace DxBlazorApplication1.Data
{
    public class Categories
    {
        public Categories()
        {
            Products = new HashSet<Products>();
        }

        public int categoryId { get; set; }
        public string categoryName { get; set; }
        public string description { get; set; }

        public virtual ICollection<Products> Products { get; set; }
    }
}
