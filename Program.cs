using Microsoft.EntityFrameworkCore;
using Scalar.AspNetCore;
using SchollApi;

var  MyAllowSpecificOrigins = "_myAllowSpecificOrigins";
var builder = WebApplication.CreateBuilder(args);

//CORS
builder.Services.AddCors(options => {
    options.AddPolicy(MyAllowSpecificOrigins, policy => {
        policy.WithOrigins("http://localhost:5173", "http://127.0.0.1:5173");
    });
});

// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddDbContext<AppDbContext>(options => {
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"));
});

//Register services
builder.Services.AddScoped<IStudenService, StudentService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
}

app.UseCors(MyAllowSpecificOrigins);

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
