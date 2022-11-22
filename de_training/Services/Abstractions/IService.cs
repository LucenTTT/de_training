namespace de_training.Services.Abstractions
{
    public interface IService<TEntity>
    {
        Task<ICollection<TEntity>> GetAllAsync();

        TEntity Insert(TEntity item);
    }
}
