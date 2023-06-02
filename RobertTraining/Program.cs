using Microsoft.EntityFrameworkCore;
using Data_Access_Layer.Data;
using Business_Logic_Layer;
using Business_Logic_Layer.Features;
using AutoMapper;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddDbContext<ContextDb>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("SQL")));

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen();

builder.Services.AddScoped< Business_Logic_Layer.Features.AccountBLL >();
builder.Services.AddScoped<Business_Logic_Layer.Features.TransactionBLL>();


builder.Services.AddAutoMapper(typeof(AutoMapperProfile));

var app = builder.Build();

if(app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
