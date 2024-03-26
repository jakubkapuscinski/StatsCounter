using Microsoft.Extensions.DependencyInjection;
using StatsCounter.Services;
using System;

namespace StatsCounter.Extensions
{
    public static class StartupExtensions
    {
        public static IServiceCollection AddGitHubService(
            this IServiceCollection services,
            Uri baseApiUrl)
        {
            services.AddHttpClient<IGitHubService, GitHubService>(client =>
            {
                client.BaseAddress = baseApiUrl;
            });

            return services;
        }
    }
}