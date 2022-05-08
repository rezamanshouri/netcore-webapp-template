using System.Text.Json.Serialization;

namespace netcore_webapp_template.Controllers.Numbers.ErrorModel
{
    public class ErrorDetails
    {
        [JsonPropertyName("code")]

        public int Code
        {
            get; set;
        }

        [JsonPropertyName("message")]
        public string Message
        {
            get; set;
        }
    }
}
