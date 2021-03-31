using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using SwaggerLesson.Filters;
using SwaggerLesson.Middleware;
using SwaggerLesson.Models;
using Swashbuckle.AspNetCore.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace SwaggerLesson
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
			services.AddLogging(config =>
			{
				config.AddConsole();
				config.AddDebug();
				config.AddEventLog();
			});
			services.AddSingleton<ILoggerFactory, LoggerFactory>();
			services.Configure<PositionOptions>(Configuration.GetSection("Position"));
			services.AddScoped<TestActionFilterAttribute>();
			services.AddScoped<TestResultServiceFilter>();
			services.AddScoped<TestExceptionFilter>();
			services.AddScoped<TestAlwaysRunResultFilter>();
			services.AddControllers(config =>
			{
				config.Filters.Add<TestActionFilter>();
			});
			services.AddHttpContextAccessor();
			services.AddSwaggerGen(c =>
			{
				c.SwaggerDoc("v1", new OpenApiInfo { Title = "SwaggerLesson", Version = "v1" });
				c.ExampleFilters();
			});
			services.AddSwaggerExamplesFromAssemblies(Assembly.GetExecutingAssembly());
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
		{
			app.UseHeaderManipulatorMiddleware(options =>
			{
				options.Add("X-Client-IP");
			});
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
				app.UseSwagger();
				app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "SwaggerLesson v1"));
			}
			app.UseForwardedHeaders();
			app.UseHttpsRedirection();
			app.UseRouting();
			app.UseAuthorization();
			app.UseOptionsVerbHandlerMiddleware();
			app.UseEndpoints(endpoints =>
			{
				endpoints.MapControllers();
			});
		}
	}
}