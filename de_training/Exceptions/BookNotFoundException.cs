using System.Net;

namespace de_training.Exceptions
{
    public class BookNotFoundException : HttpException
    {
        public BookNotFoundException() : base(HttpStatusCode.NotFound, "Book not found!")
        {
        }
    }
}
