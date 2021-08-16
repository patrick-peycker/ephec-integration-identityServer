using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;

namespace API
{
	public class Startup
	{
		public IConfiguration Configuration { get; }

		readonly string MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

		public Startup(IConfiguration configuration)
		{
			Configuration = configuration;
		}



		// This method gets called by the runtime. Use this method to add services to the container.
		public void ConfigureServices(IServiceCollection services)
		{
			// Permettre à l'Api d'être accessible par un client externe à la solution
			services.AddCors(options =>
			{
				options.AddPolicy(name: MyAllowSpecificOrigins,
						builder => builder
						.WithOrigins("http://localhost:4200")
						.AllowAnyHeader()
						.AllowAnyMethod());
			});

			services.AddControllers();

			services
				.AddAuthentication("Bearer")
				.AddJwtBearer("Bearer", options =>
				{
					options.Authority = "http://localhost:5000";
					options.RequireHttpsMetadata = false;
					options.TokenValidationParameters = new TokenValidationParameters
					{
						ValidateAudience = false
					};
				});

			services.AddAuthorization(options =>
			{
				options.AddPolicy("ApiScope", policy =>
				{
					policy.RequireAuthenticatedUser();
					policy.RequireClaim("scope", "api1");
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

			app.UseRouting();
			app.UseCors(MyAllowSpecificOrigins);
			app.UseAuthentication();
			app.UseAuthorization();

			app.UseEndpoints(endpoints =>
			{
				endpoints
					.MapControllers();
			});
		}
	}
}
