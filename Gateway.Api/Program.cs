using Prometheus;

var builder = WebApplication.CreateBuilder(args);

// -----------------------------------------------------
// CORS - NECESSÁRIO para Angular chamar o Gateway
// -----------------------------------------------------
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

// Controllers
builder.Services.AddControllers();

// Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// YARP
builder.Services.AddReverseProxy()
    .LoadFromConfig(builder.Configuration.GetSection("ReverseProxy"));

var app = builder.Build();

// -----------------------------------------------------
// Middleware
// -----------------------------------------------------
app.UseCors("AllowAll");  // <---- OBRIGATÓRIO

app.UseSwagger();
app.UseSwaggerUI();

app.MapControllers();
app.MapReverseProxy();

// endpoint de métricas do Prometheus
app.MapMetrics();

app.Run();
