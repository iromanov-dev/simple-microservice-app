using Data.Context;
using Data.UnitOfWork;
using API;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Serilog;
using Data.Repository;
using Core.Organizations.Queries.GetUsers;
using Core.Organizations.Mappings;
using FluentValidation.AspNetCore;
using MassTransit;
using Core.Organizations.Events.Publish.UserAddedEvent;
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

            services.AddDbContext<StecpointDbContext>(options => options.UseSqlServer(Configuration.GetConnectionString("DbConnectionString")));

            Log.Logger = new LoggerConfiguration().WriteTo.Console().CreateLogger();

            services.AddMediatR(typeof(GetUsersQuery).Assembly); // костыль, нужна более корректная ссылка на Core.Organizations
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

            Bus.Factory.CreateUsingRabbitMq(config =>
            {
                config.ReceiveEndpoint("users-queue", e =>
                {
                    e.Consumer<UserAddedSubscriber>();
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
            //app.UseAuthorization();


            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.UseOpenApi().UseSwaggerUi3();
        }
    }
}
