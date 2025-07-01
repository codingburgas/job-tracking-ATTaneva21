using JobTracking.DataAccess;
using JobTracking.Models;
using Microsoft.EntityFrameworkCore;

namespace JobTracking.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.AddContext();
            builder.AddIdentity();
            builder.AddCors();
            builder.AddServices();
            
            builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

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

            using (var scope = app.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                try 
                {
                    var context = services.GetRequiredService<AppDbContext>();
                    context.Database.EnsureCreated(); // Creates DB if not exists
                    
                    if (!context.Jobs.Any())
                    {
                        context.Jobs.Add(new Job { Title = "Sample Job", Status = JobStatus.Draft });
                        context.SaveChanges();
                    }
                }
                catch (Exception ex)
                {
                    var logger = services.GetRequiredService<ILogger<Program>>();
                    logger.LogError(ex, "An error occurred initializing the DB.");
                }
            }
            
            app.Run();
        }
    }
}
