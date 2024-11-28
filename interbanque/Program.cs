using interbanque.Models;
using interbanque.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddHttpClient<ApiServiceExtern>();
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.WebHost.ConfigureKestrel(options =>
{
    options.ListenAnyIP(5002); // HTTP
                               //  options.ListenAnyIP(5001, listenOptions => listenOptions.UseHttps()); // HTTPS
});

var connexBdd = builder.Configuration.GetConnectionString("connexionBdd");
builder.Services.AddDbContext<InterbanqueContext>(options => options.UseMySql(connexBdd, ServerVersion.AutoDetect(connexBdd)));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseCors(builder =>
        builder.AllowAnyOrigin()
               .AllowAnyMethod()
               .AllowAnyHeader());

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
