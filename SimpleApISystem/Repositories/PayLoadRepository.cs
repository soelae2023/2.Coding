using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using SimpleApISystem.Context;
using SimpleApISystem.Controllers;
using SimpleApISystem.Exceptions;
using SimpleApISystem.Models;

namespace SimpleApISystem.Repositories
{
    public class PayLoadRepository : IPayLoadRepository
    {
        AppDbContext _dbContext;
        private readonly ILogger<PayLoadController> _logger;
        public PayLoadRepository(AppDbContext dbContext, ILogger<PayLoadController> logger)
        {
            _dbContext = dbContext;
            _logger = logger;
        }
        public async Task<int> SavePayLoad(PayLoad payLoad)
        {
            try
            {
                _dbContext.PayLoads.Add(payLoad);
                return await _dbContext.SaveChangesAsync();
            }
            catch (DbUpdateException dbUpdateException)
            {               
                _logger.LogError(dbUpdateException, "Database update error occurred.");
                throw new DatabaseOperationException("A database update error occurred.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unexpected error occurred during database operation.");
                throw new DatabaseOperationException("An unexpected error occurred during database operation.");
            }
        }
    }
}
