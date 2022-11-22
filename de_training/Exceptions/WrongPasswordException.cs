using System.Net;

namespace de_training.Exceptions
{
    public class UserNotFoundException : HttpException
    {
        public UserNotFoundException() : base(HttpStatusCode.NotFound, "User not found")
        {
        }
    }
}
