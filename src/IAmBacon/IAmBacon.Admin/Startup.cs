﻿using System;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using IAmBacon.Core.Application.AutofacModules;
using IAmBacon.Core.Domain.ValueObject.Configuration;
using IAmBacon.Core.Infrastructure.AutofacModules;
using IAmBacon.Core.Infrastructure.Identity;
using IAmBacon.Core.Infrastructure.Post;
using IAmBacon.Core.Infrastructure.PostCategory;
using IAmBacon.Core.Infrastructure.PostTag;
using IAmBacon.Core.Infrastructure.User;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace IAmBacon.Admin
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            services
                .AddMvc(options => options.Filters.Add(new AuthorizeFilter()))
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            var config = new EmailConfiguration();
            Configuration.Bind("Email", config);
            services.AddSingleton(config);

            services.AddDbContext<CategoryContext>(options => options.UseSqlServer(Configuration.GetConnectionString("BaconSqlConnection")));
            services.AddDbContext<TagContext>(options => options.UseSqlServer(Configuration.GetConnectionString("BaconSqlConnection")));
            services.AddDbContext<PostContext>(options => options.UseSqlServer(Configuration.GetConnectionString("BaconSqlConnection")));
            services.AddDbContext<UserContext>(options => options.UseSqlServer(Configuration.GetConnectionString("BaconSqlConnection")));

            //Identity
            services.AddDbContext<IdentityContext>(options => options.UseSqlServer(Configuration.GetConnectionString("BaconSqlConnection")));

            services.AddIdentity<IdentityUser, IdentityRole>(options =>
                {
                    options.SignIn.RequireConfirmedEmail = true;
                    options.Stores.MaxLengthForKeys = 128;
                })
                .AddEntityFrameworkStores<IdentityContext>()
                .AddDefaultTokenProviders();

            services.ConfigureApplicationCookie(options =>
            {
                options.LoginPath = $"/account/login";
                options.LogoutPath = $"/account/logout";
                options.AccessDeniedPath = $"/account/access-denied";
            });

            //configure autofac

            var builder = new ContainerBuilder();

            // Register dependencies, populate the services from
            // the collection, and build the container. If you want
            // to dispose of the container at the end of the app,
            // be sure to keep a reference to it as a property or field.
            //
            // Note that Populate is basically a foreach to add things
            // into Autofac that are in the collection. If you register
            // things in Autofac BEFORE Populate then the stuff in the
            // ServiceCollection can override those things; if you register
            // AFTER Populate those registrations can override things
            // in the ServiceCollection. Mix and match as needed.
            builder.Populate(services);

            builder.RegisterModule(new CategoryModule());
            builder.RegisterModule(new CategoryCommandModule(Configuration.GetConnectionString("BaconSqlConnection")));
            builder.RegisterModule(new TagModule());
            builder.RegisterModule(new TagCommandModule());
            builder.RegisterModule(new UserCommandModule());
            builder.RegisterModule(new UserModule());
            builder.RegisterModule(new PostCommandModule());
            builder.RegisterModule(new PostModule());
            builder.RegisterModule(new EmailCommandModule());

            //var assembliesInAppDomain = AppDomain.CurrentDomain.GetAssemblies().ToArray();
            //builder.RegisterAssemblyModules(assembliesInAppDomain);

            return new AutofacServiceProvider(builder.Build());
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // https://docs.microsoft.com/en-us/aspnet/core/security/enforcing-ssl?view=aspnetcore-2.1&tabs=visual-studio#http-strict-transport-security-protocol-hsts
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseAuthentication();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Dashboard}/{action=Index}/{id?}");
            });
        }
    }
}
