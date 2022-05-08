using System.Text.Json.Serialization;

namespace netcore_webapp_template.Controllers.Numbers.ErrorModel
{
    public class Error
    {
        public Error(int code, string message) => Details = new ErrorDetails
        {
            Code = code,
            Message = message
        };

        [JsonPropertyName("error")]
        public ErrorDetails Details
        {
            get; set;
        }
    }
}
