using System.Reflection;
using api.Options;
using api.Services;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
builder.Services.AddDbContext<IDbContext, api.Services.DbContext>(options =>
                options.UseNpgsql(builder.Configuration.GetSection("DbSettings").Get<DbOptions>().ConnectionString));

builder.Services.Configure<Web3ProviderOptions>(builder.Configuration.GetSection("Infura"));
builder.Services.AddSingleton<IWeb3Provider, InfuraWeb3Provider>();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
else
{
    app.UseHttpsRedirection();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
