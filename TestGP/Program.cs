

#region  firstMAin cs
/*using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using TestGP.Models;

namespace TestGP
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Get connection string from app settings
            var constr = builder.Configuration.GetConnectionString("ConStr");

            // Add connection string 
            builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlServer(constr));

            // Add session and HttpContextAccessor
            builder.Services.AddSession(options =>
            {
                options.Cookie.Name = "MySession";
                //+ System.Guid.NewGuid().ToString("N"); // Generate unique cookie name
                options.Cookie.HttpOnly = true;
                options.IdleTimeout = System.TimeSpan.FromMinutes(20);
                options.Cookie.IsEssential = true;
                options.Cookie.SameSite = SameSiteMode.None;
            });
            builder.Services.AddHttpContextAccessor();


            // Add distributed memory cache
            builder.Services.AddDistributedMemoryCache();

            // Add services to the container.
            builder.Services.AddControllers();

            // Add CORS
            // Add CORS
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowOrigin",
                    builder =>
                    {
                        builder.AllowAnyOrigin()
                         //.WithOrigins("http://localhost:4200", "http://localhost:5200")
                         .AllowCredentials()
                         .AllowAnyMethod()
                         .AllowAnyHeader();
                    });
            });
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            // Apply CORS policy
            app.UseCors("AllowOrigin");
            app.UseCookiePolicy();
            // Add session
            app.UseSession();

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}*/

#endregion


#region token

/*using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using TestGP.Models;

namespace TestGP
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Get connection string from app settings
            var constr = builder.Configuration.GetConnectionString("ConStr");

            // Add connection string 
            builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlServer(constr));

            // Add JWT authentication
            builder.Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Secret"]))
                };
            });

            // Add distributed memory cache
            builder.Services.AddDistributedMemoryCache();

            // Add services to the container.
            builder.Services.AddControllers();

            // Add CORS
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowOrigin",
                    builder =>
                    {
                        builder.AllowAnyOrigin()
                               .AllowAnyMethod()
                               .AllowAnyHeader();
                    });
            });

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            // Apply CORS policy
            app.UseCors("AllowOrigin");

            // Use authentication
            app.UseAuthentication();

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}
*/
#endregion



using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using PasswordHandlling.Services.EmailSettings;
using TestGP.Models;

namespace TestGP
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Get connection string from app settings
            var constr = builder.Configuration.GetConnectionString("ConStr");

            // Add connection string 
            builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlServer(constr));

            // Add session and HttpContextAccessor
            /*builder.Services.AddSession(options =>
            {
                options.Cookie.Name = "MySession_" + System.Guid.NewGuid().ToString("N"); // Generate unique cookie name
                options.Cookie.HttpOnly = true;
                options.IdleTimeout = System.TimeSpan.FromMinutes(20);
            });
            builder.Services.AddHttpContextAccessor();
*/
            // Add distributed memory cache
            builder.Services.AddDistributedMemoryCache();

            // Add services to the container.
            builder.Services.AddControllers();

            // Add CORS
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowOrigin",
                    builder =>
                    {
                        builder.WithOrigins("http://localhost:4200", "http://localhost:5200","http://127.0.0.1:5500")
                               .AllowAnyMethod()
                               .AllowCredentials()
                               .AllowAnyHeader();
                    });
            });

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddTransient<IEmailSender, EmailSender>();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            // Apply CORS policy
            app.UseCors("AllowOrigin");

            // Add session
           // app.UseSession();

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}


