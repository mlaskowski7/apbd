using Tutorial7.Mappers;
using Tutorial7.Repositories;
using Tutorial7.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<ITripRepository, TripRepository>();
builder.Services.AddScoped<IClientRepository, ClientRepository>();

builder.Services.AddScoped<CountryMapper>();
builder.Services.AddScoped<TripMapper>();

builder.Services.AddScoped<ITripService, TripService>();
builder.Services.AddScoped<IClientService, ClientService>();


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