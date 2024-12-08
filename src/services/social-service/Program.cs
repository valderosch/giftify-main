using creator_service.Data;
using creator_service.Interfaces;
using creator_service.Repository;
using creator_service.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<ICommentRepository, CommentRepository>();
builder.Services.AddScoped<ICommentService, CommentService>();

builder.Services.AddScoped<ICollectionRepository, CollectionRepository>();
builder.Services.AddScoped<ICollectionService, CollectionService>();

builder.Services.AddScoped<INotificationRepository, NotificationRepository>();
builder.Services.AddScoped<INotificationService, NotificationService>();

builder.Services.AddScoped<CommentService>();
builder.Services.AddScoped<CollectionService>();
builder.Services.AddScoped<NotificationService>();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();