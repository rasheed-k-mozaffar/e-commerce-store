namespace e_commerce_store.Models.Interfaces
{
    public interface IProductRepository
    {
        Task<IEnumerable<Product>> GetAll();

        Task<Product?> GetByIdAsync(int? id);

        Task<IEnumerable<Product>> GetProductByCategory(int category);

        bool Add(Product product);

        bool Update(Product product);

        bool Delete(Product product);

        bool ProductExist(int id);

        bool Save();
    }
}