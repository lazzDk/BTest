using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using UniqueWord.Api.Extensions;
using UniqueWord.Api.Factories;
using UniqueWord.Api.Factories.Interfaces;
using UniqueWord.Api.Helper;
using UniqueWord.Api.Helper.Interfaces;
using UniqueWord.Api.Repositories;
using UniqueWord.Api.Repositories.Interfaces;
using UniqueWord.Api.Services;
using UniqueWord.Api.Services.Interfaces;

namespace UniqueWord.Api
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
            var connectionString = Configuration.GetConnectionString("DefaultConnection");

            services.AddControllers().AddNewtonsoftJson();
            services.AddNHibernate(connectionString);

            services.AddScoped<IModelFactory, ModelFactory>();

            services.AddScoped<IUniqueWordService, UniqueWordService>();

            services.AddScoped<ITextEntryRepository, TextEntryRepository>();
            services.AddScoped<IUniqueWordEntryRepository, UniqueWordEntryRepository>();
            services.AddScoped<IWatchlistRepository, WatchlistRepository>();

            services.AddScoped<ISessionHelper, SessionHelper>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
