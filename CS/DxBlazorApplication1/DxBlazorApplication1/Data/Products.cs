namespace DxBlazorApplication1.Data
{
    public class Products
    {
        public int productId { get; set; }
        public string productName { get; set; }
        public int? supplierId { get; set; }
        public int? categoryId { get; set; }
        public string quantityPerUnit { get; set; }
        public decimal? unitPrice { get; set; }
        public short? unitsInStock { get; set; }
        public short? unitsOnOrder { get; set; }
        public short? reorderLevel { get; set; }
        public bool discontinued { get; set; }
        public string ean13 { get; set; }

        public virtual Categories category { get; set; }
    }
}
