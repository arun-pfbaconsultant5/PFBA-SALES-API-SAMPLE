#region Namespace
using pfba.sales.crm.creation.Common;
using pfba.sales.crm.creation.App_Start;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using NLog.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.CookiePolicy;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using pfba.sales.crm.creation.Models.Context;
using pfba.sales.crm.creation.Handlers;
using pfba.sales.crm.creation.Middlewares;
#endregion
namespace pfba.sales.crm.creation 
{
    // 
	/// <summary>
	/// Represents the startup class fo the application.
	/// </summary>
	public class Startup
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="Startup"/> class.
		/// </summary>
		/// <param name="configuration">The configuration object.</param>
		public Startup(IConfiguration configuration)
		{
			Configuration = configuration;
		}
		/// <summary>
		/// Gets the configuration object.
		/// </summary>
		public IConfiguration Configuration { get; }

		// This method gets called by the runtime. Use this method to add services to the container.
		/// <summary>
		/// Configures the serices for the application.
		/// </summary>
		/// <param name="services">The collection of services to configure.</param>
		public void ConfigureServices(IServiceCollection services)
		{
			// AddControllers
			services.AddControllers();
			//Add Controllers with Endpoints
			services.AddControllersWithViews();
			services.AddEndpointsApiExplorer();
			services.AddSwaggerGen();
			// AddHealthChecks
			services.AddHealthChecks();
			// Dependency Injection
			DependencyInjectionConfig.AddScope(services);
			var connectionSection = Configuration.GetSection(ApplicationConstant.ConnectionStringConfigName);
			var storeProcSection = Configuration.GetSection(ApplicationConstant.StoredProcsConfigName);

			services.AddHsts(options =>
			{
				options.Preload = true;
				options.IncludeSubDomains = true;
				options.MaxAge = TimeSpan.FromDays(365);
			});

			//Binding json data into object
			services.Configure<DBContext>(connectionSection);
			services.Configure<StoredProcs>(storeProcSection);
			services.AddMemoryCache();
			// AddCors
			services.AddCors();
			services.Configure<CookiePolicyOptions>(options =>
			{
				options.MinimumSameSitePolicy = SameSiteMode.None;
				options.HttpOnly = HttpOnlyPolicy.Always;
				options.Secure = CookieSecurePolicy.Always;
			});
			services.AddAntiforgery(options =>
			{
				options.HeaderName = ApplicationConstant.XSRFHeaderName;
				options.Cookie.HttpOnly = ApplicationConstant.True;
				options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
				options.Cookie.Expiration = TimeSpan.FromHours(8);
				options.Cookie.Name = ApplicationConstant.XSRFCookieName;
				options.SuppressXFrameOptionsHeader = ApplicationConstant.False;
			});
		}
		/// <summary>
		/// Configure the application's request pipeline.
		/// </summary>
		/// <param name="app"> The application builder.</param>
		/// <param name="env">The web host environment.</param>
		/// <param name="logger">The logger manager.</param>
		public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
				app.UseSwagger();
				app.UseSwaggerUI();
			}
			else
			{
				app.ConfigureExceptionHandler(Configuration);
				app.UseHsts();
			}
			app.Use(async (context, next) =>
			{
				context.Response.Headers.Add(ApplicationConstant.XFrameOptions, ApplicationConstant.XFrameOptionsValue);
				context.Response.Headers.Add(ApplicationConstant.XContentTypeOptions, ApplicationConstant.XContentTypeOptionsValue);
				context.Response.Headers.Add(ApplicationConstant.XXssProtection, ApplicationConstant.One);
				context.Response.Headers.Add(ApplicationConstant.ReferrerPolicy, ApplicationConstant.ReferrerPolicyValue);
				context.Response.Headers.Add(ApplicationConstant.XContentSecurityPolicy, ApplicationConstant.XContentSecurityPolicyValue);
				context.Response.Headers.Add(ApplicationConstant.FeaturePolicy, ApplicationConstant.DefaultFeaturePolicy);
				await next();
			});	
			app.UseRouting();
			app.UseMiddleware<RequestMiddleware>();
			app.UseHttpsRedirection();
			app.UseAuthorization();
			app.UseCors( x => x.AllowAnyMethod().AllowAnyHeader().SetIsOriginAllowed(origin => ApplicationConstant.True));
			app.UseEndpoints(endpoints =>
			{
				endpoints.MapHealthChecks("api/Creation/health", new HealthCheckOptions()
				{
					AllowCachingResponses = ApplicationConstant.False
				});
			});
			app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
	}
}
