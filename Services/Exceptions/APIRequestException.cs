namespace Planetas_StarWars.Services.Exceptions
{
    [Serializable]
    public class APIRequestException : ApplicationException
    {
        public APIRequestException()
        {
        }

        public APIRequestException(string? message) : base(message)
        {
        }

        public APIRequestException(string? message, Exception? innerException) : base(message, innerException)
        {
        }
        
    }
}