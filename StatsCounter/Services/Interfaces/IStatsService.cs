using StatsCounter.Models;
using System.Threading.Tasks;

namespace StatsCounter.Services.Interfaces
{
    public interface IStatsService
    {
        public Task<RepositoryStats> GetRepositoryStatsByOwnerAsync(string owner);
    }
}
