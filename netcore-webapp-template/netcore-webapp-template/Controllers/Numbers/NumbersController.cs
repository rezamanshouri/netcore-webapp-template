using System.ComponentModel.DataAnnotations;

using Microsoft.AspNetCore.Mvc;

using netcore_webapp_template.Controllers.Numbers.ErrorModel;

namespace netcore_webapp_template.Controllers.Numbers
{
    [ApiController]
    [Route("numbers-api")]
    public class NumbersController : ControllerBase
    {
        private readonly Dictionary<int, string> numbers;

        private readonly ILogger<NumbersController> _logger;

        public NumbersController(ILogger<NumbersController> logger)
        {
            _logger = logger;

            numbers = new Dictionary<int, string>
            {
                [1] = "one",
                [2] = "two",
                [3] = "three"
            };
        }

        [HttpGet]
        [Route("get-letters")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(Error))]
        public IActionResult GetLetter([FromQuery][Required] int number)
        {
            if (numbers.TryGetValue(number, out var letters))
            {
                return Ok(new NumberModel
                {
                    Number = number,
                    Letter = letters
                });
            }
            else
            {
                return BadRequest(new Error(1, "Given number does not exist, you could try insert first"));
            }
        }

        [HttpPut]
        [Route("add-number")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(Error))]
        public IActionResult InsertNumber([FromBody][Required] NumberModel number)
        {
            if (numbers.ContainsKey(number.Number))
            {
                return BadRequest(new Error(2, "Given number already exist exists"));
            }
            else
            {
                // This change won't persist though since the controller's lifetime is transient.
                numbers[number.Number] = number.Letter;
                return Ok();
            }
        }
    }
}