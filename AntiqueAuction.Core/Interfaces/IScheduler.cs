using System.Threading.Tasks;

namespace AntiqueAuction.Core.Interfaces
{
    public interface IScheduler
    {
        Task Invoke();
    }
}
