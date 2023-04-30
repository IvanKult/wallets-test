using System.Reflection;
using api.Options;
using api.Services;
using Hangfire;
using Hangfire.MemoryStorage;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
builder.Services.AddDbContext<IDbContext, api.Services.DbContext>(options =>
                options.UseNpgsql(builder.Configuration.GetSection("DbSettings").Get<DbOptions>().ConnectionString), ServiceLifetime.Singleton);

builder.Services.Configure<Web3ProviderOptions>(builder.Configuration.GetSection("Infura"));
builder.Services.AddSingleton<IWeb3Provider, InfuraWeb3Provider>();
builder.Services.AddSingleton<IWalletsStorage, WalletsStorage>();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddHangfire(config => config.UseSimpleAssemblyNameTypeSerializer()
                                                    .UseDefaultTypeSerializer()
                                                    .UseMemoryStorage());
builder.Services.AddHangfireServer();

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
app.UseHangfireDashboard();

BackgroundJob.Enqueue((IWalletsStorage walletsStorage) => walletsStorage.CheckUpdates());
RecurringJob.AddOrUpdate(
                           "Run every 30 sec",
                           (IWalletsStorage walletsStorage) => walletsStorage.CheckUpdates(),
                           "*/30 * * * * *"//every 30 sec (NCrontab)
                           );


app.Run();

