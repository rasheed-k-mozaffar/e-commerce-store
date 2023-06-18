namespace e_commerce_store.Models.Interfaces
{
    public interface IDescriptionImagesRepository
    {
        public bool Add(DescriptionImages image);

        public bool Remove(DescriptionImages image);

        Task<DescriptionImages> Get(int productId ,string url);

        public bool ClearProductImages(int productId);

        public bool Save();

    }
}