using System.Collections.Generic;

namespace StatsCounter.Models
{
    public record RepositoryStats(string Owner)
    {
        public IDictionary<char, int> Letters { get; init; } = new Dictionary<char, int>();
        public double AvgStargazers { get; init; }
        public double AvgWatchers { get; init; }
        public double AvgForks { get; init; }
        public double AvgSize { get; init; }
    }
}
