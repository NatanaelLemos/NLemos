using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NLemos.Domain.Data;
using NLemos.Domain.Services;
using NLemos.Infrastructure.Data;
using NLemos.Infrastructure.Services;
using System;

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
                .AddSingleton<BlogContext>(opt => new BlogContext(connectionString));


            services
                .AddResponseCaching()
                .AddResponseCompression(opt =>
                {
                    opt.Providers.Add<GzipCompressionProvider>();
                    opt.EnableForHttps = true;
                })
                .AddMvc()
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

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseResponseCaching();
            app.UseResponseCompression();

            app.UseMvc(GetRoutes());
        }

        private static Action<IRouteBuilder> GetRoutes()
        {
            return routes =>
            {
                routes.MapRoute(
                    name: "readPost",
                    template: "Read/{hashName}",
                    defaults: new
                    {
                        controller = "Post",
                        action = "Read"
                    }
                );

                routes.MapRoute(
                    name: "paging",
                    template: "Page/{number}",
                    defaults: new
                    {
                        controller = "Post",
                        action = "Page"
                    }
                );

                routes.MapRoute(
                    name: "search",
                    template: "Search/{args}",
                    defaults: new
                    {
                        controller = "Post",
                        action = "Search"
                    }
                );

                routes.MapRoute(
                    name: "me",
                    template: "Me/{action}",
                    defaults: new
                    {
                        controller = "Me"
                    }
                );

                routes.MapRoute(
                    name: "control",
                    template: "Control/{action}",
                    defaults: new
                    {
                        controller = "Control"
                    }
                );

                routes.MapRoute(
                    name: "default",
                    template: "",
                    defaults: new
                    {
                        controller = "Post",
                        action = "Page",
                        number = 0
                    }
                );
            };
        }
    }
}