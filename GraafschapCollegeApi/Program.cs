using GraafschapCollege.Shared.Constants;
using GraafschapCollege.Shared.Interfaces;
using GraafschapCollege.Shared.Options;
using GraafschapCollegeApi.Context;
using GraafschapCollegeApi.Seeders;
using GraafschapCollegeApi.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace GraafschapCollegeApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            var services = builder.Services;

            // Add services to the container.
            services.AddControllers();

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen(options =>
            {
                options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
                {
                    In = ParameterLocation.Header,
                    Description = "Please insert JWT with Bearer into field",
                    Name = "Authorization",
                    Type = SecuritySchemeType.Http,
                    BearerFormat = "JWT",
                    Scheme = "Bearer"
                });

                options.AddSecurityRequirement(new OpenApiSecurityRequirement()
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

            // Add services to the container.
            services.AddTransient<UserService>();
            services.AddTransient<AuthService>();
            services.AddTransient<TokenService>();
            services.AddTransient<ReservationService>();
            services.AddTransient<VehicleService>();
            services.AddScoped<ICurrentUserContext, CurrentUserContext>();

            // Add database context
            services.AddDbContext<GraafschapCollegeDbContext>(options =>
            {
                //var a = builder.Configuration.GetConnectionString("DefaultConnection");
                options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection"));
            });

            // Add options pattern to the configuration
            services.Configure<JwtOptions>(builder.Configuration.GetSection(JwtOptions.SectionName));

            // Add authentication for JWT
            // Disable the default claim type mapping
            JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();
            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    // Get the IOptions from from services
                    var jwtOptions = builder.Configuration.GetSection(JwtOptions.SectionName).Get<JwtOptions>()!;

                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = jwtOptions.Issuer,
                        ValidAudience = jwtOptions.Audience,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtOptions.Key)),
                        ClockSkew = TimeSpan.Zero,
                        NameClaimType = Claims.Name,
                        RoleClaimType = Claims.Role,
                    };

                    // Disable the default claim type mapping to avoid mapping "sub" to ClaimTypes.NameIdentifier.
                    options.MapInboundClaims = false;
                });

            // Add http context accessor
            services.AddHttpContextAccessor();

            // Add authorization policy for JWT
            services.AddAuthorization();

            // Add CORS policy
            services.AddCors(options =>
            {
                options.AddDefaultPolicy(builder =>
                {
                    builder.AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader();
                });
            });

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            // Add CORS policy
            app.UseCors();

            app.UseHttpsRedirection();

            app.UseAuthentication();

            app.UseAuthorization();

            app.MapControllers();

            // Get the database context and apply migrations automatically and seed the database
            using (var scope = app.Services.CreateScope())
            {
                var scopedServices = scope.ServiceProvider;
                var dbContext = scopedServices.GetRequiredService<GraafschapCollegeDbContext>();
                dbContext.Database.Migrate();
                dbContext.Seed();
            }

            app.Run();
        }
    }
}