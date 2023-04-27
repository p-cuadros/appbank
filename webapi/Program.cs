using apibanca.application;
using apibanca.webapi.Filters;
using apibanca.webapi.Middlewares;
using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddInfrastuctureLayer(builder.Configuration);
builder.Services.AddApplicationLayer();

builder.Services.AddControllers(config => {
    config.Filters.Add(new ApiValidationFilter());
}).AddJsonOptions(options => { 
        options.JsonSerializerOptions.PropertyNamingPolicy = null; 
    });

builder.Services.Configure<ApiBehaviorOptions>(opt => { opt.SuppressModelStateInvalidFilter = true; });
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Bank Api", Version = "v1.1.1" });
    c.EnableAnnotations();
});
var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

builder.Services.AddCors(p => p.AddPolicy("corsapp", builder =>
{
    builder.WithOrigins("*").AllowAnyMethod().AllowAnyHeader();
}));

var app = builder.Build();
app.UseCors(MyAllowSpecificOrigins);

if (app.Environment.IsDevelopment() || app.Environment.IsProduction())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//app.UseHttpsRedirection();
app.UseAuthorization();
app.UseMiddleware<ExceptionHandlerMiddleware>();
app.MapControllers();
app.UseCors("corsapp");
app.Run();