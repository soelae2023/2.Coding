using SimpleApISystem.Models;

namespace SimpleApISystem.Services
{
    public interface IPayLoadService
    {
        Task<int> SavePayLoadData(PayLoad payLoad);
    }
}
