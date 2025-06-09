using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Tutorial11.Database;
using Tutorial11.Middlewares;
using Tutorial11.Repositories;
using Tutorial11.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<UsersDbContext>(opt =>
    opt.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new ArgumentException("Default Connection String must be set")));

builder.Services.AddScoped<IUserRepository, UserRepository>().AddScoped<IAuthService, AuthService>();

builder.Services.AddAuthentication(opt =>
{
    opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(opt =>
{
    opt.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateAudience = true,
        ValidateIssuer = true,
        ValidateActor = true,
        ClockSkew = TimeSpan.FromMinutes(2),
        ValidateLifetime = true,
        ValidIssuer = builder.Configuration["Jwt:Issuer"] ?? throw new ArgumentException("Configure JWT in appsettings.json"),
        ValidAudience = builder.Configuration["Jwt:Audience"] ?? throw new ArgumentException("Configure JWT in appsettings.json"),
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Secret"] ?? throw new ArgumentException("Configure JWT in appsettings.json")))
    };

    opt.Events = new JwtBearerEvents
    {
        OnAuthenticationFailed = context =>
        {
            if (context.Exception.GetType() == typeof(SecurityTokenExpiredException))
            {
                context.Response.Headers.Add("Token-Expired", "true");
            }
            return Task.CompletedTask;
        }
    };
}).AddJwtBearer("IgnoreTokenExpirationScheme", opt =>
{
    opt.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = false,
        ClockSkew = TimeSpan.FromMinutes(2),
        ValidIssuer = builder.Configuration["Jwt:Issuer"] ?? throw new ArgumentException("Configure JWT in appsettings.json"),
        ValidAudience = builder.Configuration["Jwt:Audience"] ?? throw new ArgumentException("Configure JWT in appsettings.json"),
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Secret"] ?? throw new ArgumentException("Configure JWT in appsettings.json")))
    };
});



var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwagger().UseSwaggerUI();
}

app.UseMiddleware<ErrorHandlingMiddleware>();
app.UseHttpsRedirection();

app.MapControllers();

app.Run();
