using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
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
              (options) => {
                  options.Password.RequireDigit = true;
                  options.Password.RequireLowercase = true;
                  options.Password.RequireUppercase = true;
                  options.Password.RequireNonAlphanumeric = true;
                  options.Password.RequiredLength = 8;
              }
              ).AddRoles<IdentityRole<int>>().
              AddEntityFrameworkStores<ApplicationDBContext>().AddDefaultTokenProviders();

            builder.Services.AddAuthentication(
               op
               => {
                   op.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                   op.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
               }
               ).AddJwtBearer(options => options.TokenValidationParameters = new TokenValidationParameters
               {
                   IssuerSigningKey = MyTokenHandler.GetSecurityKey(),
                   ValidateIssuer = false,
                   ValidateAudience = false,
               });


            builder.Services.AddAutoMapper(cfg => { }, typeof(MappingConfig));

            builder.Services.AddScoped<IAccountService, AccountService>();
            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();


            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
