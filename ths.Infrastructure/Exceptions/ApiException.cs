namespace ths.Infrastructure.Exceptions
{
    public class ApiException : Exception
    {
        public ApiError Error { get; }

        public ApiException(string message) : base(string.Empty)
        {
            Error = new ApiError
            {
                Message = message,
            };

        }
    }
    public class ApiError
    {
        public string Message { get; set; }
    }
}
