using System.Text;
using EstuSozluk.API.Middlewares;
using EstuSozluk.API.Repositories;
using EstuSozluk.API.Services.Abstracts;
using EstuSozluk.API.Services.Concretes;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using MySql.Data.MySqlClient;
using MySqlConnector.Logging;
using Serilog;

namespace EstuSozluk.API
{
    public class Startup
    {
        //private static readonly LoggerFactory ConsoleLoggerFactory = new LoggerFactory(providers: new[] { new ConsoleLoggerProvider((_, __) => true, true) });
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews()
            .AddNewtonsoftJson(options =>
             options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
);
            
            services.AddCors(options =>
            {
                options.AddPolicy(name: "Cors",
                    policy =>
                    {
                        policy
                            .WithOrigins("https://docs.solarproject.click", "https://www.solarproject.click", "http://localhost:3000", "https://localhost:3000", "http://localhost")
                            .AllowAnyHeader()
                            .AllowAnyMethod();
                    });
            });

            services.AddApiVersioning(setup =>
            {
                setup.DefaultApiVersion = new ApiVersion(2, 0);
                setup.AssumeDefaultVersionWhenUnspecified = true;
                setup.ReportApiVersions = true;
            });

            services.AddApiVersioning(setup =>
                setup.ApiVersionReader = new UrlSegmentApiVersionReader());

            services.AddVersionedApiExplorer(setup =>
            {
                setup.GroupNameFormat = "'v'VVV";
                setup.SubstituteApiVersionInUrl = true;
            });

            services.AddSwaggerGen();
            services.ConfigureOptions<ConfigureSwaggerOptions>();

            // services.AddTransient<MySqlConnection>(_ => new MySqlConnection(Configuration["ConnectionString:Default"]));

            /*
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options => {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = Configuration["Jwt:Issuer"],
                        ValidAudience = Configuration["Jwt:Audience"],
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Jwt:Key"]))
                    };
                });
            */
            services.AddMvc();
            services.AddControllers();
            services.AddRazorPages();

            services.AddDbContext<EstuSozlukContext>(options => {
                options.UseMySQL(Configuration["ConnectionStrings:Default"]);
            });

            services.AddScoped<ILoginService, LoginService>();
            services.AddScoped<IAuthenticationService, AuthenticationService>();
            services.AddScoped<IEntryService, EntryService>();



        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseCors("Cors");

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseSerilogRequestLogging(options =>
            {
                //// Customize the message template
                //options.MessageTemplate = "Handled {RequestPath}";

                //// Emit debug-level events instead of the defaults
                //options.GetLevel = (httpContext, elapsed, ex) => LogEventLevel.Debug;

                //// Attach additional properties to the request completion event
                //options.EnrichDiagnosticContext = (diagnosticContext, httpContext) =>
                //{
                //    diagnosticContext.Set("RequestHost", httpContext.Request.Host.Value);
                //    diagnosticContext.Set("RequestScheme", httpContext.Request.Scheme);
                //};
            });


            app.UseMiddleware<JwtMiddleware>();

            app.UseSwagger();
            //app.UseSwaggerUI(options =>
            //{
            //    options.SwaggerEndpoint("/swagger/v1/swagger.yaml", "V1");
            //    options.SwaggerEndpoint("/swagger/v2/swagger.yaml", "V2");
            //});



            app.UseRouting();
            

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapControllers();
               
            });
        }
    }
}
