using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using StatsCounter.Extensions;
using StatsCounter.Services;
using StatsCounter.Services.Interfaces;
using System;

namespace StatsCounter
{
    public class Startup
    {
        private readonly IConfiguration _configuration;

        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy("AllowedOrigins",
                    builder => builder.AllowAnyOrigin()
                                       .AllowAnyHeader()
                                       .AllowAnyMethod());
            });

            services.AddControllers();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "StatsCounter API", Version = "v1" });
            });

            services.AddGitHubService(new Uri(_configuration["GitHubSettings:BaseApiUrl"]))
                    .AddTransient<IStatsService, StatsService>();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseCors("AllowedOrigins");

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "StatsCounter API v1");
                c.RoutePrefix = string.Empty;
            });

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
