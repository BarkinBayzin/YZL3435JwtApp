using AutoMapper;
using JWTApp.BackOffice.Core.Application.Interfaces;
using JWTApp.BackOffice.Core.Application.Mappings;
using JWTApp.BackOffice.Infrastructure.Tools;
using JWTApp.BackOffice.Persistance.Context;
using JWTApp.BackOffice.Persistance.Repositories;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Reflection;
using System.Text;

var builder = WebApplication.CreateBuilder(args);
//Builder bizim servislerimizi yazacağımız alan
//Startup artık program.cs içerisine taşındı
// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Services.AddDbContext<JWTContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("conStr"));
});

builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));

builder.Services.AddMediatR(Assembly.GetExecutingAssembly()); // Assembly çalıştığında eklensin diye bu şekilde kayıt ediyorum.

builder.Services.AddAutoMapper(opt =>
{
    opt.AddProfiles(new List<Profile>()
    {
        new ProductProfile(), //buraya aynı şekilde oluşturduğumuz mapping dosyalarına ekleyebiliriz.
        new CategoryProfile(),
    });
});

builder.Services.AddCors(opt =>
{
    opt.AddPolicy("GlobalCors", config =>
    {
        config.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
    });
});

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(opt =>
{
    opt.RequireHttpsMetadata = false;
    opt.TokenValidationParameters = new TokenValidationParameters()
    {
        ValidAudience = JWTSettings.Audience,
        ValidIssuer = JWTSettings.Issuer,
        ClockSkew = TimeSpan.Zero,
        ValidateLifetime = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(JWTSettings.Key)),
        ValidateIssuerSigningKey = true
    };
});


//bu alanda eskiden hatırladığımız şekilde dependency injectionlar gerçekleştirilir.

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors("GlobalCors"); //birden fazla cors policy eklenebilir

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
