

namespace HMS.Service.Exceptions
{
    public class ConflictException : Exception
    {
        public ConflictException() : base("Room is not available.")
        {
        }
    }
}
