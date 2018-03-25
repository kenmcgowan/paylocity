using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Paylocity.Benefits.Registration.Api.Interfaces;
using Paylocity.Benefits.Registration.Api.Repositories;
using Paylocity.Benefits.Registration.Api.Services;

namespace Paylocity.Benefits.Registration.Api
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
            services.AddCors(corsOptions => corsOptions.AddPolicy("allow-all-for-demo", builder =>
            {
                builder.AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader();
            }));
            services.AddSingleton<IPersonRepository>(new InMemoryPersonRepository());
            services.AddSingleton<IPaymentCalculator>(new EvenDistributionPaymentCalculator());
            services.AddSingleton<IBenefitsService>(new BenefitsService());
            services.AddSingleton<ICompensationService>(new CompensationService());
            services.AddTransient<IRegistrationService, RegistrationService>();
            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc();
        }
    }
}
