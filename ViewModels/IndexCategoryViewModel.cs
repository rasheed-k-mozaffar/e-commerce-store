using e_commerce_store.Models;

namespace e_commerce_store.ViewModels
{
    public class IndexCategoryViewModel
    {
        public IEnumerable<Category>? Categories { get; set; }
        public int PageSize { get; set; }
        public int Page { get; set; }
        public int TotalPages { get; set; }
        public int TotalProducts { get; set; }
        public string? searchString { get; set; }
    }
}