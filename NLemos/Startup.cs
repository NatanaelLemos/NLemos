using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NLemos.Domain.Data;
using NLemos.Domain.Services;
using NLemos.Infrastructure.Data;
using NLemos.Infrastructure.Services;
using System;
using System.Globalization;

namespace NLemos
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            var connectionString = Configuration.GetConnectionString("blogContext"); //you have to configure the environment variable for this

            services
                .AddScoped<IPostRepository, PostRepositoryProxy>()
                .AddScoped<ICreatorRepository, CreatorRepositoryProxy>();
            services
                .AddScoped<IPostService, PostService>()
                .AddScoped<ISearchService, SearchService>()
                .AddScoped<ICreatorService, CreatorService>();
            services
                .AddSingleton<PostRepositoryCache>()
                .AddSingleton<CreatorCache>()
                .AddSingleton(opt => new BlogContext(connectionString));

            services
                .AddLocalization(opt => opt.ResourcesPath = "Resources");

            services
                .AddResponseCaching()
                .AddResponseCompression(opt =>
                {
                    opt.Providers.Add<GzipCompressionProvider>();
                    opt.EnableForHttps = true;
                })
                .AddMvc()
                .AddViewLocalization(LanguageViewLocationExpanderFormat.Suffix)
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            var supportedCultures = new[]
            {
                new CultureInfo("en-US"),
                new CultureInfo("pt-BR")
            };

            app.UseRequestLocalization(new RequestLocalizationOptions
            {
                DefaultRequestCulture = new RequestCulture("en-US"),
                SupportedCultures = supportedCultures,
                SupportedUICultures = supportedCultures
            });

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseResponseCaching();
            app.UseResponseCompression();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Posts}/{action=Page}/{id?}");
            });
        }
    }
}