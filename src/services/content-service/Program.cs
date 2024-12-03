using content_service.Data;
using content_service.Mapping;
using content_service.Repositories;
using content_service.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<PostRepository>();
builder.Services.AddScoped<GoalRepository>();
builder.Services.AddScoped<UserSubscriptionRepository>();
builder.Services.AddScoped<FileRepository>();

builder.Services.AddScoped<PostService>();
builder.Services.AddScoped<GoalService>();
builder.Services.AddScoped<RecommendationService>();
builder.Services.AddScoped<StorageService>();

builder.Services.AddAutoMapper(typeof(MappingProfile));
builder.Services.AddControllers();
builder.Services.AddHttpClient();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "ContentService API", Version = "v1" });
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "ContentService API v1"));
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();