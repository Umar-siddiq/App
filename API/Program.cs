using Microsoft.EntityFrameworkCore;
using Services.Interfaces;
using Services.FrontEnd;
using Utility;
using API.Controllers;
using Serilog;
using Serilog.Sinks.MSSqlServer;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<Data.EntityFramework.AppDbContext>(options =>
        options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"), b=>b.MigrationsAssembly("Data")));

builder.Services.AddScoped<APILoggingFilter>();
builder.Services.AddControllers( options =>
{
    options.Filters.Add<APILoggingFilter>();
});


builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowBlazorClient", policy =>
    {
        policy.WithOrigins("https://localhost:7214")
        .AllowAnyHeader()
        .AllowAnyMethod();
    });
});

Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .WriteTo.MSSqlServer(
        connectionString: builder.Configuration.GetConnectionString("DefaultConnection"),
        sinkOptions: new MSSqlServerSinkOptions
        { 
            TableName = "ApiLogs", AutoCreateSqlTable = true
        }).Enrich.FromLogContext().CreateLogger();

builder.Host.UseSerilog();   

builder.Services.AddMemoryCache();

builder.Services.AddScoped<IProductService, ProductService>();

builder.Services.AddAutoMapper(cfg => { }, typeof(AutoMapperProfile));

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors("AllowBlazorClient");

app.UseAuthorization();

app.MapControllers();

app.Run();