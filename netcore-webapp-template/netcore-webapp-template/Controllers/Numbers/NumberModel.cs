
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace netcore_webapp_template.Controllers.Numbers
{
    public class NumberModel
    {
        [JsonPropertyName("letter")]
        [Required]
        public string Letter
        {
            get; set;
        }

        [JsonPropertyName("number")]
        [Required]
        public int Number
        {
            get; set;
        }
    }
}
