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
using TotvsChallenge.Data.Contracts;
using TotvsChallenge.DataAccess;
using TotvsChallenge.DataAccess.Repositories;

namespace TovusChallenge
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
            services.AddDbContext<DataContext>(options => options
                .UseSqlServer(Configuration.GetConnectionString("Sql")));

            services.AddMvc()
                .AddFluentValidation(opt => opt.RegisterValidatorsFromAssembly(Assembly.Load("TotvsChallenge.Core")));
            FluentValidation.ValidatorOptions.LanguageManager.Culture = new CultureInfo("es");
            #region if need to register validators from somewhere else..
            //services.AddMvc().AddFluentValidation();
            //services.AddTransient<IValidator<BuyRequest>, BuyValidator>();
            //services.AddTransient<IValidator<FindOperationByIdRequest>, FindOperationByIdRequestValidator>();
            //services.AddTransient<IValidator<FindClientInfoByIdRequest>, FindClientInfoByIdRequestValidator>();
            #endregion

            services.AddTransient<IOperationRepository, OperationRepository>();
            services.AddTransient<IClientRepository, ClientRepository>();
            services.AddScoped<IConnectionFactory, ConnectionFactory>();
            services.AddScoped<IBaseRepository, BaseRepository>();
            services.AddScoped<IReportRepository, ReportRepository>();


            //services.AddMediatR(typeof(Startup));
            var assembly = AppDomain.CurrentDomain.Load("TotvsChallenge.Core");
            services.AddMediatR(assembly);

            services.AddSingleton<Serilog.ILogger>(opt =>
            {
                var connStr = Configuration["ConnectionStrings:Sql"];
                var tableName = Configuration["ConnectionStrings:LogTable"];
                return new LoggerConfiguration().WriteTo.
                MSSqlServer(connStr, tableName, restrictedToMinimumLevel: Serilog.Events.LogEventLevel.Warning,
                autoCreateSqlTable: true).CreateLogger();
            });

            //services.AddSingleton<ILoggerFactory, LoggerFactory>();
            //services.AddSingleton(typeof(ILogger<>), typeof(Logger<>));

            services.AddControllers();

            services.AddEasyCaching(opt =>
            {
                opt.UseRedis(redisConfig =>
                {
                    redisConfig.DBConfig.Endpoints.Add(new ServerEndPoint("localhost", 6379));
                    redisConfig.DBConfig.AllowAdmin = true;
                }
                , "localRedis");
            });

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Totvs Challenge", Version = "v1" });
            });
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
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
            });
        }
    }
}
