namespace de_training.Services.Abstractions
{
    public interface IBookService : IService<Book>
    {
        Task<Book> GetBookAsync(long id);
        Task RemoveBookAsync(long id);
        Task<Book> CreateBookAsync(Book book);
    }
}
