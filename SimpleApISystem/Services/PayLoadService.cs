using SimpleApISystem.Controllers;
using SimpleApISystem.Exceptions;
using SimpleApISystem.Models;
using SimpleApISystem.Repositories;

namespace SimpleApISystem.Services
{
    public class PayLoadService : IPayLoadService
    {
        private readonly IPayLoadRepository _payLoadRepository;
        private readonly ILogger<PayLoadController> _logger;
        public PayLoadService(IPayLoadRepository payLoadRepository, ILogger<PayLoadController> logger)
        {
            _payLoadRepository = payLoadRepository;
            _logger = logger;
        }
        public async Task<int> SavePayLoadData(PayLoad payLoad)
        {
            try
            {
                return await _payLoadRepository.SavePayLoad(payLoad);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unexpected error occurred during database operation.");
                throw new DatabaseOperationException("An unexpected error occurred during database operation.");
            }

        }
    }
}
