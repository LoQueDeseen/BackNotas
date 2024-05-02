using BackNotas.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddControllers();

builder.Services.AddDbContext<NotasContext>(options => 
    options.UseMySql(
        builder.Configuration.GetConnectionString("MysqlConnection"),
        Microsoft.EntityFrameworkCore.ServerVersion.Parse("8.0.2-mysql")
    ));

builder.Services.AddCors(options=> {
    options.AddPolicy("Policy", n => {
        n.AllowAnyOrigin()
        .AllowAnyHeader()
        .AllowAnyMethod();

    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors("Policy");

app.MapControllers();


app.Run();


