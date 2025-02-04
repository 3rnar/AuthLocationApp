using Serilog;
using AuthLocationApp.Api.DependencyInjection;
using AuthLocationApp.Api.Middlewares;
using AuthLocationApp.Application.DependencyInjection;
using AuthLocationApp.Infrastructure.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilog((ctx, lc) =>
 lc.ReadFrom.Configuration(builder.Configuration)
   .Enrich.FromLogContext());

builder.Services.AddWebApiServices();
builder.Services.AddApplicationServices();
builder.Services.AddInfrastructure(builder.Configuration, builder.Environment);
builder.Services.AddMappingServices();

var app = builder.Build();

await app.Services.InitializeDatabaseAsync();

app.UseMiddleware<ExceptionMiddleware>();
app.UseSerilogRequestLogging();

app.UseDefaultFiles();
app.UseStaticFiles();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.MapFallbackToFile("/index.html");

app.Run();
