using LoggerService;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.HttpOverrides;
using MovieApp.Extensions;
using NLog;

var builder = WebApplication.CreateBuilder(args);

string nlogConfigPath = string.Concat(Directory.GetCurrentDirectory(), "/nlog.config");
LogManager.LoadConfiguration(nlogConfigPath);

builder.Services.ConfigureSwagger();
builder.Services.AddEndpointsApiExplorer();
builder.Services.ConfigureCors();
builder.Services.ConfigureIISIntegration();
builder.Services.ConfigureServices();
builder.Services.ConfigureFluentMigrator(builder.Configuration);
builder.Services.AddAuthentication();
builder.Services.ConfigureIdentity(builder.Configuration);
builder.Services.ConfigureJWT(builder.Configuration);
builder.Services.AddResponseCaching();
builder.Services.ConfigureControllers();
builder.Services.AddAutoMapper(typeof(Program));
builder.Services.Configure<FormOptions>(options =>
{
    options.MultipartBodyLengthLimit = 200000000; 
});

var app = builder.Build();


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseForwardedHeaders(new ForwardedHeadersOptions
{
    ForwardedHeaders = ForwardedHeaders.All
});

app.UseCors("CorsPolicy");
app.UseResponseCaching();
 
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

var logger = app.Services.GetRequiredService<ILoggerManager>();

app.ConfigureExceptionHandler(logger);
app.MigrateDatabase(logger);

app.UseSwagger();
app.UseSwaggerUI();

app.Run();
