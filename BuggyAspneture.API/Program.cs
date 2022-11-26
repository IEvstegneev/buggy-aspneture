using BuggyAspneture.DataAccess.PostgreSQL;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Получить доступ к EntityFramework
// > dotnet new tool-manifest
// > dotnet tool install dotnet-ef
// Подключение контекста 
//  > dotnet ef dbcontext info --project BuggyAspneture.API
// Создание миграций (-s - startup проект где инжектится ДБ-контекст, -p - project где миграции)
//  > dotnet ef migrations add Init -s .\BuggyAspnetyre.API\ -p BuggyAspneture.DataAccess.PostgreSQL
// Показать скрипт миграции
//  > dotnet ef migrations script 0 -s BuggyAspneture.API -p BuggyAspneture.DataAccess.PostgreSQL
builder.Services.AddDbContext<BuggyAspnetureDbContext>(options =>
{
    options.UseNpgsql(builder.Configuration.GetConnectionString(nameof(BuggyAspnetureDbContext)));
});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
