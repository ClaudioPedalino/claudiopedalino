using EasyCaching.Core.Configurations;
using FluentValidation.AspNetCore;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Serilog;
using System;
using System.Globalization;
using System.Reflection;
using TotvsChallengePoC.Data.Contracts;
using TotvsChallengePoC.Data.EF;
using TotvsChallengePoC.Data.EF.Repository;
using TotvsChallengePoC.Data.Repositories;

namespace TotvsChallengePoC
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            #region DockerImage => https://hub.docker.com/repository/registry-1.docker.io/17891789/totvschallengepocapi/tags?page=1
            #endregion


            services.AddDbContext<DataContext>(options => options
                .UseSqlServer(Configuration.GetConnectionString("Sql")));

            services.AddMvc()
                .AddNewtonsoftJson()
                .AddFluentValidation(opt => opt.RegisterValidatorsFromAssembly(Assembly.Load("TotvsChallengePoC.Core")));

            //services.AddValidatorsFromAssembly(typeof(Startup).Assembly);
            //services.AddValidatorsFromAssembly(Assembly.Load("TotvsChallengePoC.Core"));

            FluentValidation.ValidatorOptions.LanguageManager.Culture = new CultureInfo("es");

            AddContracts(services);

            services.AddMediatR(AppDomain.CurrentDomain.Load("TotvsChallengePoC.Core"));

            services.AddSingleton<Serilog.ILogger>(opt =>
            {
                return new LoggerConfiguration().WriteTo.
                    MSSqlServer(Configuration["ConnectionStrings:Sql"],
                                Configuration["ConnectionStrings:LogTable"],
                                restrictedToMinimumLevel: Serilog.Events.
                                LogEventLevel.Warning,
                                autoCreateSqlTable: true)
                    .CreateLogger();
            });


            services.AddControllers();

            services.AddEasyCaching(opt =>
            {
                opt.UseRedis(redisConfig =>
                {
                    redisConfig.DBConfig.Endpoints
                        .Add(new ServerEndPoint(Configuration["Redis:Host"],
                        Convert.ToInt32(Configuration["Redis:Port"])));
                    redisConfig.DBConfig.AllowAdmin = true;
                }
                , Configuration["Redis:Name"]);
            });

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Totvs Challenge Api", Version = "v1" });
            });
        }

        private static void AddContracts(IServiceCollection services)
        {
            services.AddTransient<IOperationRepository, OperationRepository>();
            services.AddTransient<IClientRepository, ClientRepository>();
            services.AddScoped<IConnectionFactory, ConnectionFactory>();
            services.AddScoped<IBaseRepository, BaseRepository>();
            services.AddScoped<IReportRepository, ReportRepository>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Totvs Challenge Api");
            });
        }
    }
}
