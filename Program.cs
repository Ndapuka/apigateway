using Ocelot.DependencyInjection;
using Ocelot.Middleware;
using Ocelot.Provider.Polly;

var builder = WebApplication.CreateBuilder(args);
builder.Configuration.AddJsonFile("ocelot.json", optional: false, reloadOnChange: true);

builder.Services.AddCors(options => {
    options.AddPolicy("CorsPolicy", builder => {
        builder
        .WithOrigins("http://localhost:4200")
        .AllowAnyMethod()
        .AllowAnyHeader()
        .AllowCredentials()
        .SetIsOriginAllowed(origin => true); // for dev
    });
});

builder.Services
    .AddOcelot()
    .AddPolly();


var app = builder.Build();
// Enable CORS
app.UseCors("CorsPolicy");

await app.UseOcelot();

app.Run();
