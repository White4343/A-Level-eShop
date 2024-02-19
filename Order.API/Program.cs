using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Order.API.Data;
using Order.API.Models;
using Order.API.Repositories;
using Order.API.Repositories.Interfaces;
using Order.API.Services;
using Order.API.Services.Interfaces;
using IBasketService = Order.API.Services.Interfaces.IBasketService;

namespace Order.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var configuration = GetConfiguration();

            WebApiLinks.BasketApi = configuration["BasketApi"];

            var builder = WebApplication.CreateBuilder(args);

            var authority = configuration["IdentityServer:Authority"];

            builder.Services.AddAuthentication("Bearer")
                .AddJwtBearer("Bearer", options =>
                {
                    options.Audience = "orderapi";
                    options.Authority = authority;

                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateAudience = false
                    };

                    options.RequireHttpsMetadata = false;
                });

            builder.Services.AddAuthorization(options =>
            {
                options.AddPolicy("Order.FullAccess", policy =>
                {
                    policy.RequireAuthenticatedUser();
                    policy.RequireClaim("scope", "order.fullaccess");
                });
                options.AddPolicy("Order.Client", policy =>
                {
                    policy.RequireAuthenticatedUser();
                    policy.RequireClaim("scope", "order.client");
                });
            });

            // Add services to the container.

            builder.Services.AddAutoMapper(typeof(Program));

            builder.Services.AddHttpClient();
            builder.Services.AddTransient<IBasketService, BasketService>();

            builder.Services.AddTransient<IOrderRepository, OrderRepository>();
            builder.Services.AddTransient<IOrderService, OrderService>();

            builder.Services.AddTransient<IOrderBasketRepository, OrderBasketRepository>();
            builder.Services.AddTransient<IOrderBasketService, OrderBasketService>();

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo { Title = "Order.API", Version = "v1" });

                options.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
                {
                    Type = SecuritySchemeType.OAuth2,
                    Flows = new OpenApiOAuthFlows()
                    {
                        Implicit = new OpenApiOAuthFlow()
                        {
                            AuthorizationUrl = new Uri($"{authority}/connect/authorize"),
                            TokenUrl = new Uri($"{authority}/connect/token"),
                            Scopes = new Dictionary<string, string>
                            {
                                { "order.fullaccess", "Order API" },
                                { "order.client", "Client Order API" }
                            }
                        }
                    }
                });

                options.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme()
                        {
                            Reference = new OpenApiReference()
                            {
                                Id = "oauth2",
                                Type = ReferenceType.SecurityScheme
                            }
                        },
                        new[] { "order.fullaccess", "order.client" }
                    }
                });
            });

            builder.Services.AddDbContext<AppDbContext>(options =>
                options.UseNpgsql(builder.Configuration.GetConnectionString("ConnectionString")));

            builder.Services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy", policy =>
                {
                    policy.SetIsOriginAllowed((host => true))
                        .AllowAnyHeader()
                        .AllowAnyMethod()
                        .AllowCredentials();
                });
            });

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI(setup =>
                {
                    setup.SwaggerEndpoint($"{configuration["PathBase"]}/swagger/v1/swagger.json", "Order.API v1");
                    setup.OAuthClientId("orderswaggerui");
                    setup.OAuthAppName("Order Swagger UI");
                });
            }

            app.UseHttpsRedirection();
            app.UseCors("CorsPolicy");

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllers();

            CreateDbIfNotExists(app);
            app.Run();

            IConfiguration GetConfiguration()
            {
                var builder = new ConfigurationBuilder()
                    .SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile("appsettings.json", false, true)
                    .AddEnvironmentVariables();

                return builder.Build();
            }

            void CreateDbIfNotExists(IHost host)
            {
                using (var scope = host.Services.CreateScope())
                {
                    var services = scope.ServiceProvider;
                    try
                    {
                        var context = services.GetRequiredService<AppDbContext>();

                        DbInnit.InnitAsync(context).Wait();
                    }
                    catch (Exception ex)
                    {
                        var logger = services.GetRequiredService<ILogger<Program>>();
                        logger.LogError(ex, "An error occurred creating the DB.");
                    }
                }
            }
        }
    }
}
