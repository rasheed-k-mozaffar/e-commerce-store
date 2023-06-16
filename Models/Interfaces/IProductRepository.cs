namespace e_commerce_store.Models.Interfaces
{
    public interface IProductRepository
    {
        Task<IEnumerable<Product>> GetAll();

        Task<Product?> GetByIdAsync(int? id);
        Task<IEnumerable<Product>> GetSliceAsync(int offset, int size);

        Task<IEnumerable<Product>> GetProductByCategory(int category);

        bool Add(Product product);

        bool Update(Product product);

        bool Delete(Product product);

        bool ProductExist(int id);

        bool Save();

        Task<int> GetCountAsync();
        Task<int> GetCountBySearchAsync(string searchString);
        Task<int> GetCountBySearchWithCategoryAsync(string searchString , int CategoryId);

        Task<IEnumerable<Product>> GetProductsByCategoryAndSliceAsync(int categoryID, int offset, int size);

        Task<IEnumerable<Product>> GetProductsByPriceAndSliceAsync(int priceMax,int priceMin,int categoryID, int offset, int size);
        Task<IEnumerable<Product>>  SearchAndSliceAsync(string searchString , int offset, int size);
        Task<IEnumerable<Product>> SearchByCategoryAndSliceAsync(string searchString , int categoryId , int offset, int size);
        Task<int> GetCountByCategoryAsync(int categoryID);
        Task<int> GetCountByPriceAsync(int categoryID, int priceMax,int priceMin);
    }
}