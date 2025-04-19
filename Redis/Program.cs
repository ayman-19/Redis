using Microsoft.AspNetCore.Antiforgery;
using Microsoft.EntityFrameworkCore;
using Redis.CashService;
using Redis.Models;
using StackExchange.Redis;

namespace Redis
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            builder.Services.AddDbContext<RevisionContext>(options =>
                options.UseNpgsql(
                    "Host=redis.database;Port=5432;Database=redis;Username=ayman;Password=ayman"
                )
            );
            builder.Services.AddMemoryCache();
            builder.Services.AddScoped<MemoryCacheService>();
            builder.Services.AddStackExchangeRedisCache(cfg =>
            {
                cfg.Configuration = builder.Configuration.GetConnectionString("redis");
            });
            //builder.Services.AddSingleton<IConnectionMultiplexer>(cfg =>
            //{
            //    var config = ConfigurationOptions.Parse(
            //        builder.Configuration.GetConnectionString("redis") ?? ""
            //    );
            //    return ConnectionMultiplexer.Connect(config);
            //});
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddScoped<ICashService, CacheService02>();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            app.UseStaticFiles();
            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapControllers();
            app.UseRouting();

            app.Run();
        }
    }
}
