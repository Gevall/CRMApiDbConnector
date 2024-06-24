
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using WebApiDbConnector.Context;

namespace WebApiDbConnector
{
    public class Program
    {
        public static void Main(string[] args)
        {
            
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddAuthorization();

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            //builder.Configuration
            //    .SetBasePath(Directory.GetCurrentDirectory())
            //    .AddJsonFile("appsettings.json", false, true)
            //    .AddEnvironmentVariables();

            //builder.Services.AddDbContext<TripsContext>(opt =>
            //opt.UseNpgsql("name=TripsDatabase"));

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();

            var summaries = new[]
            {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

            app.MapGet("/weatherforecast", (HttpContext httpContext) =>
            {
                var forecast = Enumerable.Range(1, 5).Select(index =>
                    new WeatherForecast
                    {
                        Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                        TemperatureC = Random.Shared.Next(-20, 55),
                        Summary = summaries[Random.Shared.Next(summaries.Length)]
                    })
                    .ToArray();
                return forecast;
            })
            .WithName("GetWeatherForecast")
            .WithOpenApi();

            var context = new TripsContext();
            app.MapGet("/gettrips", () =>
            {
                var getData = from trips in context.Trips
                              join managers in context.Managers on trips.ManagerId equals managers.Id
                              join employes in context.Employes on trips.EmployeId equals employes.Id
                              join company in context.Companies on trips.CompanyId equals company.Id
                              join triptype in context.Triptypes on trips.TripTypeId equals triptype.Id
                              select new
                              {
                                  id = trips.Id,
                                  managerName = $"{managers.Lastname.Trim()} {managers.Firstname.Trim()} {managers.Patronymic.Trim()}",
                                  employe = $"{employes.Lastname.Trim()} {employes.Firstname.Trim()} {employes.Patronymic.Trim()}",
                                  dateOfTrip = trips.TripDate,
                                  company = company.CompanyName.Trim(),
                                  deadLine = trips.DeadlineContract,
                                  customer = trips.Customer.Trim(),
                                  address = trips.CustomerAddress.Trim(),
                                  caption = trips.Caption.Trim()
                              };
                return getData.ToList();
            });

            app.Run();
        }
    }
}