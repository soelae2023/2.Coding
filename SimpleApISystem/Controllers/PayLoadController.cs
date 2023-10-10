using Microsoft.AspNetCore.Mvc;
using SimpleApISystem.Exceptions;
using SimpleApISystem.Models;
using SimpleApISystem.RequestModels;
using SimpleApISystem.Services;

namespace SimpleApISystem.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PayLoadController : ControllerBase
    {
        private readonly ILogger<PayLoadController> _logger;
        private readonly IPayLoadService _payLoadService;
        public PayLoadController(ILogger<PayLoadController> logger, IPayLoadService payLoadService)
        {
            _logger = logger;
            _payLoadService = payLoadService;
        }

        [HttpPost]
        public async Task<IActionResult> PostPayLoad(PayloadModel payload)
        {
            if (payload == null || payload.Data == null)
            {
                throw new PayloadValidationException("Invalid payload: Data or payload is missing.");
            }

            var dataToStore = new PayLoad
            {
                Temperature = payload.Data.Temperature,
                Humidity = payload.Data.Humidity,
                Occupancy = payload.Data.Occupancy
            };

            _logger.LogInformation("Save payload..");

            var result = await _payLoadService.SavePayLoadData(dataToStore);

            if (result > 0)
            {
                _logger.LogInformation("Save Success..");
                return Ok(new { message = "Payload data received and stored successfully." });
            }
            else
            {
                throw new DatabaseOperationException("Failed to save payload data to the database.");
            }
        }
    }
}