namespace e_commerce_store.Models.Interfaces
{
    public interface IDescriptionImagesRepository
    {
        public bool Add(DescriptionImages image);

        public bool Remove(DescriptionImages image);

        public bool Save();

    }
}