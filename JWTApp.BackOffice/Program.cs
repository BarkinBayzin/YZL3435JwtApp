using AutoMapper;
using JWTApp.BackOffice.Core.Application.Interfaces;
using JWTApp.BackOffice.Core.Application.Mappings;
using JWTApp.BackOffice.Persistance.Context;
using JWTApp.BackOffice.Persistance.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

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


//bu alanda eskiden hatırladığımız şekilde dependency injectionlar gerçekleştirilir.

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
