using SimpleApISystem.Models;

namespace SimpleApISystem.Repositories
{
    public interface IPayLoadRepository
    {
        Task<int> SavePayLoad(PayLoad payLoad);
    }
}
