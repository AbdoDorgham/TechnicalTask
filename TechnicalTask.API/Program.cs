using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using TechnicalTask.BusinessLogic.Entities.General;
using TechnicalTask.BusinessLogic.Interfaces.IRepositories;
using TechnicalTask.BusinessLogic.Interfaces.IServices;
using TechnicalTask.BusinessLogic.Interfaces.IUnitOfWorks;
using TechnicalTask.BusinessLogic.MapperConfigration;
using TechnicalTask.BusinessLogic.Services;
using TechnicalTask.BusinessLogic.Utils;
using TechnicalTask.DataAccess.Data;
using TechnicalTask.DataAccess.UnitOfWorks;

namespace TechnicalTask.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddDbContext<ApplicationDBContext>(options => options.UseLazyLoadingProxies().UseSqlServer(builder.Configuration.GetConnectionString("TechTaskDB")));
            builder.Services.AddIdentityCore<ApplicationUser>(
              (options) =>
              {
                  options.Password.RequireDigit = true;
                  options.Password.RequireLowercase = true;
                  options.Password.RequireUppercase = true;
                  options.Password.RequireNonAlphanumeric = true;
                  options.Password.RequiredLength = 8;
              }
              ).AddRoles<IdentityRole<int>>().
              AddEntityFrameworkStores<ApplicationDBContext>().AddDefaultTokenProviders();

            builder.Services.AddAuthentication(opt => opt.DefaultAuthenticateScheme = "defSheme")
                .AddJwtBearer("defSheme", op => {
                    op.TokenValidationParameters = new TokenValidationParameters()
                    {
                        ValidateIssuer = false,
                        ValidateAudience = false,
                        ValidateIssuerSigningKey = true,
                        ValidateLifetime = true,
                        IssuerSigningKey = MyTokenHandler.GetSecurityKey()
                    };
                });

            builder.Services.AddAutoMapper(cfg => { }, typeof(MappingConfig));

            builder.Services.AddScoped<IAccountService, AccountService>();
            builder.Services.AddScoped<IOrderService, OrderService>();
            builder.Services.AddScoped<ICustomerService, CustomerService>();
            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
           

            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowAll", policy =>
                {
                    policy
                        .AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader();
                });
            });

            var app = builder.Build();

            // Apply migrations at runtime
            using (var scope = app.Services.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDBContext>();
                dbContext.Database.Migrate(); 
            }
            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseCors("AllowAll");




            app.MapControllers();

            app.Run();
        }
    }
}
