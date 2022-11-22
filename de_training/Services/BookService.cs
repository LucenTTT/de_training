using de_training.Exceptions;
using de_training.Services.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace de_training.Services
{
    public class BookService : BaseService<Book>, IBookService
    {
        public BookService(libraryContext libraryContext) : base(libraryContext)
        {
        }

        public async Task<Book> GetBookAsync(long id)
        {
            var book = await _dbSet.FirstOrDefaultAsync(x => x.BookId == id);
            if (book == null)
            {
                throw new BookNotFoundException();
            }

            return book;
        }

        public async Task<Book> CreateBookAsync(Book book)
        {
            var insertedBook = _dbSet.Add(book).Entity;
            await _libraryContext.SaveChangesAsync();
            return insertedBook;
        }

        public async Task RemoveBookAsync(long id)
        {
            var book = await GetBookAsync(id);
            _dbSet.Remove(book);
        }
    }
}
