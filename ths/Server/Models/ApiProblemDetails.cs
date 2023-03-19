namespace ths.Server.Models
{
    public class ApiProblemDetails : HttpValidationProblemDetails
    {
        public string error { get; set; }
        public ApiProblemDetails(string error)
        {
            this.error = error;
        }
    }
}
