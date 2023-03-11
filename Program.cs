using System.Reflection;
using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;

using Serilog;
using MediatR;

using TaskManagement.Common;
using TaskManagement.DB;

// create the web server builder
var builder = WebApplication.CreateBuilder(args);

#region Configure Serilog Logging:
var logger = new LoggerConfiguration()
    .ReadFrom.Configuration(builder.Configuration)
    .Enrich.FromLogContext()
    .CreateLogger();
builder.Logging.AddSerilog(logger);
#endregion

#region Add services to the container:
var assembly = Assembly.GetExecutingAssembly();
var assemblyName = assembly.GetName().Name;

// Register ResultService
builder.Services.AddSingleton<IResultService, ResultService>();

// Register the db context
var dbConnection = builder.Configuration.GetConnectionString("Local");
builder.Services.AddDbContext<TaskManagementContext>(options => options.UseSqlServer(dbConnection));
// Register DB Service
builder.Services.AddScoped<IDBService, DBService>();

// Register Mapster
builder.Services.AddMapster();

// Register MediatR
builder.Services.AddMediatR(assembly);

// Allow CORS
builder.Services.AddCors();

// Add API Controllers and Configure JSON options.
builder.Services
    .AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.IncludeFields = true;
        options.JsonSerializerOptions.WriteIndented = true;
        options.JsonSerializerOptions.AllowTrailingCommas = true;
        options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
        options.JsonSerializerOptions.NumberHandling = JsonNumberHandling.AllowNamedFloatingPointLiterals;
    });

// Register swagger APIs docs
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(config =>
    config.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, $"{assemblyName}.xml"))
);
#endregion

// build the web server
var app = builder.Build();

// initialize AutoMapperProfile with FileService
FileService.Initialize(app.Services.CreateScope().ServiceProvider.GetRequiredService<IWebHostEnvironment>());

#region Configure the HTTP request pipeline:
// global exception handler
app.UseMiddleware<ErrorHandlerMiddleware>();

// db seeder
await app.UseDbSeeder();

// swagger API docs
app.UseSwagger();
app.UseSwaggerUI();

// allow CORS
app.UseCors(config => config
    .AllowAnyMethod()
    .AllowAnyHeader()
    .SetIsOriginAllowed(origin => true)
    .AllowCredentials()
);

// API routes
app.MapControllers();

// Runs the web Server
app.Run();
#endregion
