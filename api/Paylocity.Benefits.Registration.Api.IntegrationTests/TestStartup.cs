using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Paylocity.Benefits.Registration.Api.Interfaces;
using Paylocity.Benefits.Registration.Api.Repositories;
using Paylocity.Benefits.Registration.Api.Services;

namespace Paylocity.Benefits.Registration.IntegrationTests
{
    public class TestStartup
    {
        public TestStartup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<IPersonRepository>(
                new InMemoryPersonRepository(TestDataStores.EmployeeStore, TestDataStores.DependentStore));
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
