using InmoNovara.Models;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Hosting;

namespace InmoNovara
{
    public class Startup
    {
        private readonly IConfiguration Configuration;

        public Startup(IConfiguration configuration)
        {
            this.Configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
              services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
				.AddCookie(options =>
				{
					options.LoginPath = "/Usuario/Login";
					options.LogoutPath = "/Usuario/Logout";
					options.AccessDeniedPath = "/Home/Restringido";
				})
				.AddJwtBearer(options =>//la api web valida con token
				{
					options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
					{
						ValidateIssuer = true,
						ValidateAudience = true,
						ValidateLifetime = true,
						ValidateIssuerSigningKey = true,
						ValidIssuer = Configuration["TokenAuthentication:Issuer"],
						ValidAudience = Configuration["TokenAuthentication:Audience"],
						IssuerSigningKey = new SymmetricSecurityKey(System.Text.Encoding.ASCII.GetBytes(
							Configuration["TokenAuthentication:SecretKey"])),
					};
					options.Events = new JwtBearerEvents
					{
						OnMessageReceived = context =>
						{
							// Read the token out of the query string
							var accessToken = context.Request.Query["access_token"];
							// If the request is for our hub...
							var path = context.HttpContext.Request.Path;
							if (!string.IsNullOrEmpty(accessToken) &&
								path.StartsWithSegments("/chatsegurohub"))
							{//reemplazar la url por la usada en la ruta ???
								context.Token = accessToken;
							}
							return Task.CompletedTask;
						}
					};
				});

              services.AddAuthorization(options => 
              {
                options.AddPolicy("Administrador",
                                    policy => policy.RequireRole("Administrador"));
              });      
              services.AddMvc();
              
            
            /* PARA MySql - usando Pomelo */
			services.AddDbContext<DataContext>(
				options => options.UseMySql(
					Configuration["ConnectionStrings:DefaultConnection"],
					ServerVersion.AutoDetect(Configuration["ConnectionStrings:DefaultConnection"])
				)
			);  
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            // app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute("Login", "entrar/{**accion}", new { controller = "Usuario", action = "Login" });
                endpoints.MapControllerRoute(name: "default","{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
