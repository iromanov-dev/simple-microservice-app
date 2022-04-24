using Data.Context;
using Data.UnitOfWork;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using MediatR;
using Serilog;
using Core.Organizations.Queries.GetUsers;
using Core.Organizations.Mappings;
using FluentValidation.AspNetCore;
using MassTransit;
using Core.Organizations.Events.Subscribers.UserAdded;

namespace Organizations.API
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
            services.AddControllers();

            services.AddScoped<IUnitOfWork, UnitOfWork>();

            var connectionString = Environment.GetEnvironmentVariable("DB_CONNECTION_STRING");

            services.AddDbContext<StecpointDbContext>(options => options.UseNpgsql(connectionString));

            Log.Logger = new LoggerConfiguration().WriteTo.Console().CreateLogger();

            services.AddMediatR(typeof(GetUsersQuery).Assembly);
            services.AddAutoMapper(typeof(OrganizationsMapper));
            services.AddMvc().AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<GetUsersQueryValidator>());

            services.AddSwaggerDocument(config =>
                config.PostProcess = document =>
                {
                    document.Info.Version = "v1";
                    document.Info.Title = "Organizations API";
                    document.Info.Title = "Stecpoint demo service 2";
                }
            );

            services.AddMassTransit(config =>
            {
                config.AddConsumer<UserAddedSubscriber>();
                config.UsingRabbitMq((ctx, cfg) =>
                {
                    cfg.Host(Environment.GetEnvironmentVariable("EVENT_BUS_HOST"));

                    cfg.ReceiveEndpoint("users-queue", c =>
                    {
                        c.ConfigureConsumer<UserAddedSubscriber>(ctx);
                    });
                });
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

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.UseOpenApi().UseSwaggerUi3();

            using (var scope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope())
            {
                scope.ServiceProvider.GetRequiredService<StecpointDbContext>().Database.Migrate();
            }
        }
    }
}
