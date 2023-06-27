using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Api_Auth.Context;
using Api_Auth.Interfaces;
using Api_Auth.Services;
using Api_Auth.Extensions;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;
var connectionStringNovaGlass = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddAuthentication(configuration);
builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlServer(connectionStringNovaGlass));



var mapperConfig = new MapperConfiguration(m =>
{

});

IMapper mapper = mapperConfig.CreateMapper();


builder.Services.AddScoped<IClienteService, ClienteService>();

builder.Services.AddCors(options => options.AddPolicy("AllowWebapp",
                                    builder => builder.AllowAnyOrigin()
                                                    .AllowAnyHeader()
                                                    .AllowAnyMethod()));

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
var app = builder.Build();
app.UseSwagger();
app.UseSwaggerUI();

app.UseCors("AllowWebapp");
app.UseAuthorization();

app.MapControllers();

app.Run();

