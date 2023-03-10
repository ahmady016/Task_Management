
using System.Text.Json.Serialization;
using System.Reflection;
using Serilog;
using MediatR;

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

#region Configure the HTTP request pipeline:

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
