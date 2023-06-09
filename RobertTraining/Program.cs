﻿using Business_Logic_Layer;
using Business_Logic_Layer.Features.Account;
using Business_Logic_Layer.Features.Transaction;
using Data_Access_Layer.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System;
using Business_Logic_Layer.Features.Account.Models;
using Business_Logic_Layer.Features.Account.Queries;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddDbContext<ContextDb>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("SQL")));
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen();

builder.Services.AddScoped<AccountBLL>();
builder.Services.AddScoped<TransactionBLL>();

builder.Services.AddMediatR(cfg => {
    cfg.RegisterServicesFromAssembly(typeof(AutoMapperProfile).Assembly);
});

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
