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
                var Origins = Configuration["AllowedOrigins"].Split(";");
                options.AddPolicy(name: "Cors",
                    policy =>
                    {
                        policy
                            .WithOrigins(Origins)
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

            services.AddMvc();
            services.AddControllers();

            services.AddDbContext<EstuSozlukContext>(options =>
            {
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

            app.UseSerilogRequestLogging(options => { });

            app.UseMiddleware<JwtMiddleware>();

            app.UseSwagger();
            //app.UseSwaggerUI(options =>
            //{
            //    options.SwaggerEndpoint("/swagger/v1/swagger.yaml", "V1");
            //    options.SwaggerEndpoint("/swagger/v2/swagger.yaml", "V2");
            //});

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
