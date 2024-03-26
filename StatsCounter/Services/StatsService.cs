using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using StatsCounter.Models;
using StatsCounter.Services.Interfaces;

namespace StatsCounter.Services
{
    public class StatsService : IStatsService
    {
        private readonly IGitHubService _gitHubService;

        public StatsService(IGitHubService gitHubService)
        {
            _gitHubService = gitHubService;
        }

        public async Task<RepositoryStats> GetRepositoryStatsByOwnerAsync(string owner)
        {
            var repos = await _gitHubService.GetRepositoryInfosByOwnerAsync(owner);

            if (!repos.Any())
            {
                return new RepositoryStats(owner)
                {
                    AvgStargazers = 0,
                    AvgWatchers = 0,
                    AvgForks = 0,
                    AvgSize = 0,
                    Letters = new Dictionary<char, int>()
                };
            }

            var stats = new RepositoryStats(owner)
            {
                AvgStargazers = repos.Average(repo => repo.StargazersCount),
                AvgWatchers = repos.Average(repo => repo.WatchersCount),
                AvgForks = repos.Average(repo => repo.ForksCount),
                AvgSize = repos.Average(repo => repo.Size),
                Letters = repos.SelectMany(repo => repo.Name.ToLower())
                               .Where(char.IsLetter)
                               .GroupBy(c => c)
                               .ToDictionary(group => group.Key, group => group.Count())
            };

            return stats;
        }
    }
}
