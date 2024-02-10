namespace Catalog.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var configuration = GetConfiguration();

            var builder = WebApplication.CreateBuilder(args);

            var authority = configuration["IdentityServer:Authority"];

            builder.Services.AddAuthentication("Bearer")
                .AddJwtBearer("Bearer", options =>
                {
                    options.Audience = "catalogapi";
                    options.Authority = authority;

                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateAudience = false
                    };

                    options.RequireHttpsMetadata = false;
                });

            builder.Services.AddAuthorization(options =>
            {
                options.AddPolicy("Catalog.FullAccess", policy =>
                {
                    policy.RequireAuthenticatedUser();
                    policy.RequireClaim("scope", "catalog.fullaccess");
                });
            });

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            


            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo { Title = "Catalog.API", Version = "v1" });

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
                                { "catalog.fullaccess", "Catalog API" }
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
                        new[] { "catalog.fullaccess" }
                    }
                });
            });

            // Add services to the container.

            builder.Services.AddAutoMapper(typeof(Program));

            builder.Services.AddTransient<IBrandRepository, BrandRepository>();
            builder.Services.AddTransient<IBrandService, BrandService>();

            builder.Services.AddTransient<ITypeRepository, TypeRepository>();
            builder.Services.AddTransient<ITypeService, TypeService>();

            builder.Services.AddTransient<IItemRepository, ItemRepository>();
            builder.Services.AddTransient<IItemService, ItemService>();

            builder.Services.AddTransient<IBffItemRepository, BffItemRepository>();
            builder.Services.AddTransient<IBffItemService, BffItemService>();

            // Connection to postgres database
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
                    setup.SwaggerEndpoint($"{configuration["PathBase"]}/swagger/v1/swagger.json", "Catalog.API v1");
                    setup.OAuthClientId("catalogswaggerui");
                    setup.OAuthAppName("Catalog Swagger UI");
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
