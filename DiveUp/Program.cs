//using DiveUp.Data;
//using Microsoft.EntityFrameworkCore;
//using Microsoft.Extensions.DependencyInjection;

//namespace DiveUp
//{
//    public class Program
//    {
//        public static void Main(string[] args)
//        {
//            var builder = WebApplication.CreateBuilder(args);

//            // Controllers
//            builder.Services.AddControllers();

//            // Swagger / OpenAPI
//            builder.Services.AddEndpointsApiExplorer();
//            builder.Services.AddSwaggerGen();
//            /*
//                        builder.Services.AddDbContext<AppDbContext>(options =>
//                            options.UseSqlServer(builder.Configuration.GetConnectionString("DiveUp")));*/


//            /*     builder.Services.AddDbContext<AppDbContext>(options =>
//                   options.UseNpgsql(builder.Configuration.GetConnectionString("DiveUp")));*/
//            /*            var connStr =
//                     builder.Configuration.GetConnectionString("DiveUp") ??
//                     builder.Configuration["DATABASE_URL"];

//                        builder.Services.AddDbContext<AppDbContext>(options =>
//                            options.UseNpgsql(connStr));*/

//            var conn =
//    builder.Configuration.GetConnectionString("DiveUp")
//    ?? builder.Configuration["ConnectionStrings:DiveUp"]
//    ?? builder.Configuration["ConnectionStrings__DiveUp"]
//    ?? builder.Configuration["DATABASE_URL"];

//            if (string.IsNullOrWhiteSpace(conn))
//                throw new Exception("DB connection string is missing from Railway env.");

//            builder.Services.AddDbContext<AppDbContext>(options =>
//                options.UseNpgsql(conn));



//            var app = builder.Build();

//            ///*   // Swagger in Development
//            //   if (app.Environment.IsDevelopment())
//            //   {
//            //       app.UseSwagger();
//            //       app.UseSwaggerUI();
//            //   }*/


//            app.UseSwagger();
//            app.UseSwaggerUI();

//            app.UseHttpsRedirection();

//            app.UseAuthorization();

//            app.MapControllers();

//            using (var scope = app.Services.CreateScope())
//            {
//                var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
//                db.Database.Migrate();
//            }

//            app.Run();
//        }
//    }
//}


using DiveUp.Data;
using Microsoft.EntityFrameworkCore;
using Npgsql;

namespace DiveUp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

            var builder = WebApplication.CreateBuilder(args);

            // Controllers
            builder.Services.AddControllers();

            // Swagger / OpenAPI
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            // ✅ Railway: always prefer env var DATABASE_URL (or ConnectionStrings__DiveUp) over appsettings.json
            // Railway often provides DATABASE_URL as a URL like: postgresql://user:pass@host:port/db
            var rawConn =
                Environment.GetEnvironmentVariable("DATABASE_URL")
                ?? Environment.GetEnvironmentVariable("ConnectionStrings__DiveUp")
                ?? builder.Configuration["DATABASE_URL"]
                ?? builder.Configuration["ConnectionStrings__DiveUp"]
                ?? builder.Configuration["ConnectionStrings:DiveUp"]
                ?? builder.Configuration.GetConnectionString("DiveUp");

            if (string.IsNullOrWhiteSpace(rawConn))
                throw new Exception("DB connection string is missing. Set DATABASE_URL in Railway Variables.");

            var conn = NormalizePostgresConnectionString(rawConn);

            builder.Services.AddDbContext<AppDbContext>(options =>
                options.UseNpgsql(conn));

            var app = builder.Build();

            app.UseSwagger();
            app.UseSwaggerUI();

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapControllers();

            // Apply EF migrations on startup
            using (var scope = app.Services.CreateScope())
            {
                var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
                db.Database.Migrate();
            }

            app.Run();
        }

        private static string NormalizePostgresConnectionString(string conn)
        {
            // If it's already a classic Npgsql connection string, return it.
            if (!conn.StartsWith("postgres://", StringComparison.OrdinalIgnoreCase)
                && !conn.StartsWith("postgresql://", StringComparison.OrdinalIgnoreCase))
            {
                return conn;
            }

            // Convert Railway URL format to Npgsql connection string
            // Example: postgresql://user:pass@host:port/db
            var uri = new Uri(conn.Replace("postgres://", "postgresql://", StringComparison.OrdinalIgnoreCase));
            var userInfo = uri.UserInfo.Split(':', 2);

            var csb = new NpgsqlConnectionStringBuilder
            {
                Host = uri.Host,
                Port = uri.Port,
                Database = uri.AbsolutePath.Trim('/'),
                Username = userInfo.Length > 0 ? userInfo[0] : string.Empty,
                Password = userInfo.Length > 1 ? userInfo[1] : string.Empty,

                // If you ever switch to DATABASE_PUBLIC_URL, you may need SSL:
                // SslMode = SslMode.Require,
                // TrustServerCertificate = true,
            };

            return csb.ConnectionString;
        }
    }
}
