
using Pharmacy_managment.Services.InvoiceServices;
using Pharmacy_managment.Services.PharmacistServices;
using Pharmacy_managment.Services.PurChaseOrderServices;

namespace Pharmacy_managment
{
    public static class DependencyInjection
    {
        public static IServiceCollection Dependencies(this IServiceCollection services,IConfiguration configuration)
        {
            services.Swagger()
                .ConnectionString(configuration)
                .Mapster()
                .Services()
                .Validation()
                .Identity()
                .JWT(configuration);
            services.AddControllers();
            services.AddExceptionHandler<GlobalExceptionHandler>();
            services.AddProblemDetails();
            services.AddBackgroundJobsConfig(configuration);
            services.AddHttpContextAccessor();

            services.Configure<MailSettings>(configuration.GetSection(nameof(MailSettings)));
            
            return services;
        }
        public static IServiceCollection Swagger(this IServiceCollection services)
        {
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen(c =>
            {
                c.AddSecurityDefinition("Bearer", new Microsoft.OpenApi.Models.OpenApiSecurityScheme
                {
                    Description = "JWT Authorization header using the Bearer scheme. Example: \"Authorization: Bearer {token}\"",
                    Name = "Authorization",
                    In = Microsoft.OpenApi.Models.ParameterLocation.Header,
                    Type = Microsoft.OpenApi.Models.SecuritySchemeType.ApiKey,
                    Scheme = "Bearer"
                });

                c.AddSecurityRequirement(new Microsoft.OpenApi.Models.OpenApiSecurityRequirement()
        {
            {
                new Microsoft.OpenApi.Models.OpenApiSecurityScheme
                {
                    Reference = new Microsoft.OpenApi.Models.OpenApiReference
                    {
                        Type = Microsoft.OpenApi.Models.ReferenceType.SecurityScheme,
                        Id = "Bearer"
                    },
                    Scheme = "oauth2",
                    Name = "Bearer",
                    In = Microsoft.OpenApi.Models.ParameterLocation.Header,
                },
                new System.Collections.Generic.List<string>()
            }
        });
            });
            return services;
        }
        public static IServiceCollection ConnectionString(this IServiceCollection services,IConfiguration configuration)
        {
            var connectionString =configuration.GetConnectionString("Pharmacy");
            services.AddDbContext<ApplicationDbcontext>(options => options.UseSqlServer(connectionString));
            return services;
        }
        public static IServiceCollection Mapster(this IServiceCollection services)
        {


            var mapingConfig = TypeAdapterConfig.GlobalSettings;
            mapingConfig.Scan(Assembly.GetExecutingAssembly());
            services.AddSingleton<IMapper>(new Mapper(mapingConfig));


            return services;
        }
        public static IServiceCollection Services(this IServiceCollection services)
        {
            services.AddScoped<ISupplierService, SupplierService>();
            services.AddScoped<InvoiceServices, InvoiceService>();
            services.AddScoped<IPurChaseOrderService, PurChaseOrderService>();
            services.AddScoped<IPharmacistService, PharmacistService>();
            services.AddScoped<ICustomerService, CustomerService>();
            services.AddScoped<IMedicineBatchService,MedicineBatchService>();
            services.AddScoped<IMedicineService, MedicineService>();    
            services.AddScoped<IEmailSender, EmailService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<ICategoryService, CategoryService>();
            return services;
        }
        public static IServiceCollection Validation(this IServiceCollection services)

        {


            services.AddValidatorsFromAssemblyContaining<Program>();
            object value = services.AddFluentValidationAutoValidation();



            return services;
        }
        public static IServiceCollection Identity(this IServiceCollection services)
        {

            services.AddIdentity<ApplicationUser, ApplicationRole>()
           .AddEntityFrameworkStores<ApplicationDbcontext>()
           .AddDefaultTokenProviders();


            return services;
        }
        public static IServiceCollection JWT(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IAuthService, AuthService>();
            services.AddOptions<JwtOptions>()
                .BindConfiguration(JwtOptions.SectionName)
                .ValidateDataAnnotations()
                .ValidateOnStart();
            var JwtSettings = configuration.GetSection(JwtOptions.SectionName).Get<JwtOptions>();
            services.AddSingleton<IJwtProvider, JwtProvider>();
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
                .AddJwtBearer(options =>
                {
                    options.SaveToken = true;
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(JwtSettings?.Key!)),
                        ValidIssuer = JwtSettings?.Issure,
                        ValidAudience = JwtSettings?.Audience,
                    };

                });
            services.Configure<IdentityOptions>(options =>
            {
                options.Password.RequiredLength = 8;
                options.SignIn.RequireConfirmedEmail = true;
                options.User.RequireUniqueEmail = true;
            });

            return services;
        }

        private static IServiceCollection AddBackgroundJobsConfig(this IServiceCollection services,
        IConfiguration configuration)
        {
            services.AddHangfire(config => config
                .SetDataCompatibilityLevel(CompatibilityLevel.Version_180)
                .UseSimpleAssemblyNameTypeSerializer()
                .UseRecommendedSerializerSettings()
                .UseSqlServerStorage(configuration.GetConnectionString("Pharmacy")));
            services.AddHangfireServer();

            return services;
        }

    }
}
