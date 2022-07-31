using AdminEntity.Infrastructure;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace EmpCRUD_API
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
            services.AddCors(options =>

            {

                options.AddPolicy(

                name: "AllowOrigin",

                builder => {

                    builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();

                });

            });
            services.AddControllers();
            var key = Configuration["JWT:Key"];
            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

            }).AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(Configuration["JWT:Key"])),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            });
            services.AddControllers().AddNewtonsoftJson();
            services.AddSingleton<IJWTAuthenticationManager>(new JWTAuthenticationManager(key, Configuration));

            //services.AddDbContext<ADMIN_MODELS>(o =>
            //{
            //    o.UseSqlServer(_configuartion["connectionStrings:EmployeeDbConnectionString"]);
            //});

            //string mySqlConnectionStr = Configuration.GetConnectionString("DefaultConnection");
            //services.AddDbContextPool<ADMIN_MODELS>(options => options.UseMySql(mySqlConnectionStr,
            //    Pomelo.EntityFrameworkCore.MySql.Storage.ServerVersion.AutoDetect(mySqlConnectionStr)));

            //    services.AddDbContext<ADMIN_MODELS>(options =>
            //options.UseMySql(Configuration.GetConnectionString("ConnectionStrings:Default")));



            //services.AddTransient<MySqlConnection>(_ => new MySqlConnection(Configuration["ConnectionStrings:Default"]));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseCors("AllowOrigin");
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

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
