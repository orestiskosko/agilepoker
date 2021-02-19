using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Sapp.Api.Extensions;
using Sapp.Common;
using Sapp.Core;
using Sapp.Core.Entities;
using Sapp.Core.Hubs;
using Sapp.Core.Interfaces;
using Sapp.Core.Mappers;
using Sapp.Core.Persistence;
using Sapp.Core.Services;
using Sapp.Core.Workers;

namespace Sapp.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration, IHostEnvironment environment)
        {
            Configuration = configuration;
            Environment = environment;
        }

        public IConfiguration Configuration { get; }
        public IHostEnvironment Environment { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddApiContext(Configuration);
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IRoomService, RoomService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IMapper<Room, RoomDto>, RoomDtoMapper>();
            services.AddScoped<IMapper<User, UserDto>, UserDtoMapper>();

            services.AddHostedService<JanitorWorker>();

            services.AddControllers().AddNewtonsoftJson();
            services.AddSignalR().AddNewtonsoftJsonProtocol();
            services.AddCors(
                options =>
                {
                    options.AddDefaultPolicy(
                        builder =>
                        {
                            builder.WithOrigins(
                                    "http://localhost:5000",
                                    "https://sapp-web.netlify.app",
                                    "https://agileplanner.netlify.app")
                                .AllowAnyMethod().AllowAnyHeader().AllowCredentials();
                        });
                });
            services.AddSwaggerGen(
                c =>
                {
                    c.SwaggerDoc(
                        "v1",
                        new OpenApiInfo
                        {
                            Title = "Sapp.Api",
                            Version = "v1"
                        });
                });
            services.AddHttpClient();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.MigrateDatabase();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Sapp.Api v1"));

            app.UseRouting();
            app.UseCors();
            // app.UseAuthorization();

            app.UseEndpoints(
                endpoints =>
                {
                    endpoints.MapControllers();
                    endpoints.MapHub<VotingHub>("/votinghub");
                });
        }
    }
}