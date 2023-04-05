using bArtTestTask.WebAPI.DB;
using bArtTestTask.WebAPI.Extensions;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;
var services = builder.Services;
// Add services to the container.

services.AddDbContext<BArtTestTaskDbContext>(o =>
{
    o.UseSqlServer(configuration["ConnectionStrings:SQLServer"]);
});

services.AddScoped<DbContext, BArtTestTaskDbContext>();
services.AddRepositories();
services.AddAutoMapper();
services.AddServices();

services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
services.AddEndpointsApiExplorer();
services.AddSwaggerGen();

services.AddCors(options => options.AddPolicy("DevPolicy", builder =>
{
    builder.AllowAnyHeader()
        .AllowAnyMethod()
        .SetIsOriginAllowed(_ => true)
        .AllowCredentials();
}));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
