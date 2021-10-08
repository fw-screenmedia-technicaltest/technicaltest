using System.Threading.Tasks;

namespace ScreenMediaTT.Core.Interfaces
{
    public interface IUtilityService
    {
        Task<bool> SeedAsync();
        bool Reset();
    }
}
