using DiveUp.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace DiveUp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Controllers
            builder.Services.AddControllers();

            // Swagger / OpenAPI
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            /*
                        builder.Services.AddDbContext<AppDbContext>(options =>
                            options.UseSqlServer(builder.Configuration.GetConnectionString("DiveUp")));*/


            builder.Services.AddDbContext<AppDbContext>(options =>
              options.UseNpgsql(builder.Configuration.GetConnectionString("DiveUp")));

            var app = builder.Build();

            ///*   // Swagger in Development
            //   if (app.Environment.IsDevelopment())
            //   {
            //       app.UseSwagger();
            //       app.UseSwaggerUI();
            //   }*/


            app.UseSwagger();
            app.UseSwaggerUI();

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapControllers();

            using (var scope = app.Services.CreateScope())
            {
                var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
                db.Database.Migrate();
            }

            app.Run();
        }
    }
}
