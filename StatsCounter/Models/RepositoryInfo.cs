using System.Text.Json.Serialization;

namespace StatsCounter.Models
{
    public record RepositoryInfo
    {
        [JsonPropertyName("id")] public long Id { get; init; }

        [JsonPropertyName("name")] public string Name { get;  init;} = string.Empty;

        [JsonPropertyName("stargazers_count")] public long StargazersCount { get;  init;}

        [JsonPropertyName("watchers_count")] public long WatchersCount { get;  init;}

        [JsonPropertyName("forks_count")] public long ForksCount { get;  init;}

        [JsonPropertyName("size")] public long Size { get;  init;}
    }
}