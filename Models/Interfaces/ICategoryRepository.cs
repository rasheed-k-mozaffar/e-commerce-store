using System.Collections.Immutable;
namespace e_commerce_store.Models.Interfaces
{
    public interface ICategoryRepository
    {
        List<Category> GetAll();

        Task<Category?> GetByIdAsync(int? id);

        Task<IEnumerable<Category>> GetSliceAsync(int offset, int size);
        Task<IEnumerable<Category>>  SearchAndSliceAsync(string searchString , int offset, int size);
        Task<int> GetCountAsync();
        Task<int> GetCountBySearchAsync(string searchString);
        bool Add(Category category);

        bool Update(Category category);

        bool Delete(Category category);

        bool CategoryExist(int id);

        bool Save();
    }
}