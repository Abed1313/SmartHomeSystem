using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using SmartHomeSystem.Data;
using SmartHomeSystem.Models;
using SmartHomeSystem.Repository.Interface;
using SmartHomeSystem.Repository.Services;

namespace SmartHomeSystem
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Configure the database context.
            string BuilderVar = builder.Configuration.GetConnectionString("DefaultConnection");
            builder.Services.AddDbContext<SmartHomeDbContext>(optionX => optionX.UseSqlServer(BuilderVar));

        

            builder.Services.AddIdentity<Characters, IdentityRole>()
                .AddEntityFrameworkStores<SmartHomeDbContext>()
                 .AddDefaultTokenProviders(); //Secrch About this line(i used it for ForgotPassword) --> // Adding default token providers 

            builder.Services.AddScoped<IAcountUser, AccountUserService>();
            builder.Services.AddScoped<IAdmin, AdminService>();
            builder.Services.AddScoped<IGuest, GuestService>();
            builder.Services.AddScoped<IProvider, ProviderService>();
            builder.Services.AddScoped<JwtTokenServeses>();
            
            // Add controllers to services
            builder.Services.AddControllers();

            // Add authentication using JWT
            builder.Services.AddAuthentication(options =>
            {
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = JwtTokenServeses.ValidateToken(builder.Configuration);
            });

            // Add Swagger
            builder.Services.AddSwaggerGen(option =>
            {
                option.SwaggerDoc("SmartHomeAPI", new OpenApiInfo()
                {
                    Title = "Smart Home Api Doc",
                    Version = "v1",
                    Description = "Api for managing all Smart Home"
                });

                option.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.Http,
                    Scheme = "bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description = "Please enter user token below."
                });

                option.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        }
                    },
                    Array.Empty<string>()
                }
            });
            });

            var app = builder.Build();

            app.UseAuthentication();
            app.UseAuthorization(); // Ensure authorization middleware is used

            // Use Swagger and Swagger UI
            app.UseSwagger(options =>
            {
                options.RouteTemplate = "api/{documentName}/swagger.json";
            });

            app.UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint("/api/SmartHomeAPI/swagger.json", "Smart Home API v1");
                options.RoutePrefix = "";
            });

            // Map controllers
            app.MapControllers();

            app.MapGet("/", () => "Hello World!");

            app.Run();
        }
    }

}
