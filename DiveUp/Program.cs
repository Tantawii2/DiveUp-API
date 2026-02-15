using DiveUp.Data;
using Microsoft.EntityFrameworkCore;

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

            builder.Services.AddDbContext<AppDbContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("DiveUp")));

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

            app.Run();
        }
    }
}
