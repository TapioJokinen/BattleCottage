using BattleCottage.Core.Entities;
using BattleCottage.Data;
using BattleCottage.Data.Repositories;
using BattleCottage.Data.Repositories.UserRepository;
using BattleCottage.Services.Authentication;
using BattleCottage.Services.Games;
using BattleCottage.Services.HealthCheck;
using BattleCottage.Services.LFGPosts;
using BattleCottage.Services.RAWG;
using BattleCottage.Services.Token;
using BattleCottage.Web.ExceptionFilter;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

var builder = WebApplication.CreateBuilder(args);

// CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy(
        name: MyAllowSpecificOrigins,
        policy =>
        {
            policy
                .WithOrigins(
                    "http://localhost:3000",
                    "https://localhost:7069",
                    "https://battlecottage.com",
                    "https://www.battlecottage.com"
                )
                .AllowAnyMethod()
                .AllowAnyHeader()
                .AllowCredentials();
        }
    );
});

// Filters
builder.Services.AddControllers(options =>
{
    options.Filters.Add(new BaseExceptionFilterAttribute());
});

// Database context
builder.Services.AddDbContext<ApplicationDbContext>();

// Repositories
builder.Services.AddScoped(typeof(IRepository<>), typeof(EntityRepository<>));

// Routes
builder.Services.Configure<RouteOptions>(options =>
{
    options.LowercaseUrls = true;
});

// Identity
builder.Services
    .AddIdentity<User, IdentityRole>(options =>
    {
        options.User.RequireUniqueEmail = true;
    })
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultTokenProviders();

// Authentication
builder.Services
    .AddAuthentication(options =>
    {
        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
    })
    // Adding Jwt Bearer
    .AddJwtBearer(options =>
    {
        options.SaveToken = true;
        options.RequireHttpsMetadata = false;
        options.TokenValidationParameters = new TokenValidationParameters()
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidAudience = builder.Configuration["JWT:ValidAudience"],
            ValidIssuer = builder.Configuration["JWT:ValidIssuer"],
            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(
                    builder.Configuration["JWT:Secret"] ?? throw new ArgumentException("JWT Secret not found.")
                )
            ),
            ClockSkew = TimeSpan.Zero,
            ValidateLifetime = true,
        };
    });

// Redis
builder.Services.AddStackExchangeRedisCache(options =>
{
    options.Configuration = builder.Configuration.GetConnectionString(
        builder.Configuration["ConnectionStrings:Redis"]
            ?? throw new ArgumentException("Redis connection string not found.")
    );
    options.InstanceName = "BattleCottage";
});

builder.Services.AddHttpClient();

// Services
builder.Services.AddHostedService<ConsumeRAWGGamesService>();

builder.Services.AddScoped<IHealthCheckService, HealthCheckService>();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<ITokenService, TokenService>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IRAWGGamesService, RAWGGamesService>();
builder.Services.AddScoped<IGameService, GameService>();
builder.Services.AddScoped<ILFGPostService, LFGPostService>();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseHttpsRedirection();
}

if (!app.Environment.IsDevelopment())
{
    app.UseForwardedHeaders(
        new ForwardedHeadersOptions
        {
            ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
        }
    );
}

app.UseCors(MyAllowSpecificOrigins);

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();

public partial class Program { }
