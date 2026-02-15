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


            /*     builder.Services.AddDbContext<AppDbContext>(options =>
                   options.UseNpgsql(builder.Configuration.GetConnectionString("DiveUp")));*/
            /*            var connStr =
                     builder.Configuration.GetConnectionString("DiveUp") ??
                     builder.Configuration["DATABASE_URL"];

                        builder.Services.AddDbContext<AppDbContext>(options =>
                            options.UseNpgsql(connStr));*/

            var conn =
    builder.Configuration.GetConnectionString("DiveUp")
    ?? builder.Configuration["ConnectionStrings:DiveUp"]
    ?? builder.Configuration["ConnectionStrings__DiveUp"]
    ?? builder.Configuration["DATABASE_URL"];

            if (string.IsNullOrWhiteSpace(conn))
                throw new Exception("DB connection string is missing from Railway env.");

            builder.Services.AddDbContext<AppDbContext>(options =>
                options.UseNpgsql(conn));



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
