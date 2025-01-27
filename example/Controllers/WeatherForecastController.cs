using BackgroundTask.Interfaces;
using Example.Examples.BackgroundTasks;
using Microsoft.AspNetCore.Mvc;

namespace example.Controllers {
    [ApiController]
    [Route( "[controller]" )]
    public class WeatherForecastController : ControllerBase {
        private readonly ILogger<WeatherForecastController> _logger;
        private readonly IBackgroundCommandProcessor _Processor;

        public WeatherForecastController( ILogger<WeatherForecastController> logger , IBackgroundCommandProcessor processor ) {
            _Processor = processor;
            _logger = logger;
        }

        [HttpGet( "test/{Number1}/{Number2}" )]
        public async Task<List<object>> Get( [FromRoute] float Number1 , [FromRoute] float Number2 ) {
            await _Processor.SendAsync( new SumNumbersTask { Number1 = Number1 , Number2 = Number2 } , CancellationToken.None );

            return new List<object>();
        }
    }
}
