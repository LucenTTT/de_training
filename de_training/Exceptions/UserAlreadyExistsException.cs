using System.Net;

namespace de_training.Exceptions
{
    public class UserAlreadyExistsException : HttpException
    {
        public UserAlreadyExistsException() : base(HttpStatusCode.Conflict, "User with this name already exists!")
        {
        }
    }
}
