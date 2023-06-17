using e_commerce_store.Models;

namespace e_commerce_store.ViewModels
{
    public class IndexProductViewModel
    {
        public IEnumerable<Product>? Products { get; set; }
        public List<Category>? Categories { get; set; }
        public int PageSize { get; set; }
        public int Page { get; set; }
        public int TotalPages { get; set; }
        public int TotalProducts { get; set; }
        public int CategoryId { get; set; }

        public string? searchString { get; set; }
    }
}