using Microsoft.EntityFrameworkCore;
using ReservationApi.Application.Repository;
using ReservationApi.Infrastructure;
using ReservationApi.Infrastructure.Entities;
using System.Reflection;

namespace ReservationApi
{
    public partial class Program
    {
        private static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(options =>
            {
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                options.IncludeXmlComments(xmlPath);
            });

            builder.Services.AddDbContext<ReservationDbContext>((cfg) =>
            {
                cfg.UseSqlServer(builder.Configuration.GetConnectionString("ReservationDbContext"));
            });

            builder.Services.AddScoped<IRepository<Resource>, ResourceRepository>();
            builder.Services.AddScoped<IRepository<Reservation>, ReservationRepository>();

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
        }
    }
}