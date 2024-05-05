using BackNotas.Data;
using Microsoft.EntityFrameworkCore;

using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.Text;


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

//creacion de  servicio de politicas politocas 
builder.Services.AddCors(options=> {
    options.AddPolicy("Policy", n => { // puedo poner el nombre que me de la gana 
        n.AllowAnyOrigin()// cualquier front 
        .AllowAnyHeader()
        .AllowAnyMethod();

    });
});    

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = "tu_issuer",
            ValidAudience = "tu_audience",
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("tu_clave_secreta"))
        };
    });    

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
//usar la politica
app.UseCors("Policy");

// Usar la autenticación
app.UseAuthentication();

app.UseCors("Policy");

app.MapControllers();


app.Run();


