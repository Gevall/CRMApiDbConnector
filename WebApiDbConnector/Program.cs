
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using System.Net;
using System.Text.Json;
using WebApiDbConnector.Context;
using WebApiDbConnector.Models;

namespace WebApiDbConnector
{
    public class Program
    {
        public static readonly ILoggerFactory loggerFactory = LoggerFactory.Create(builder => builder.AddConsole());

        public static void Main(string[] args)
        {
            var context = new TripsContext(); // Контекст базы

            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddAuthorization();
            builder.Services.AddHttpClient();

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


            app.MapGet("/gettrips", () =>
            {
                ILogger logger = loggerFactory.CreateLogger("GetTrips");

                var getData = from trips in context.Trips
                              join managers in context.Managers on trips.ManagerId equals managers.Id
                              join employes in context.Employes on trips.EmployeId equals employes.Id into employeeGroup
                              from subEmployes in employeeGroup.DefaultIfEmpty()
                              join company in context.Companies on trips.CompanyId equals company.Id
                              join triptype in context.Triptypes on trips.TripTypeId equals triptype.Id 
                              select new
                              {
                                  id = trips.Id,
                                  managerName = $"{managers.Lastname.Trim()} {managers.Firstname.Trim()} {managers.Patronymic.Trim()}",
                                  employe = subEmployes != null ? $"{subEmployes.Lastname.Trim()} {subEmployes.Firstname.Trim()} {subEmployes.Patronymic.Trim()}" : "",
                                  dateOfTrip = trips.TripDate.Value.ToShortDateString(),
                                  company = company.CompanyName.Trim(),
                                  deadLine = trips.DeadlineContract.Value.ToShortDateString(),
                                  customer = trips.Customer.Trim(),
                                  address = trips.CustomerAddress.Trim(),
                                  caption = trips.Caption.Trim()
                              };
                logger.LogInformation("TestLogger: {0}", DateTime.Now.ToShortTimeString());
                return getData.ToList();
            });

            app.MapGet("/getemployes", () =>
            {
                //ILogger logger = loggerFactory.CreateLogger("GetTrips");

                var getdata = from employes in context.Employes
                              select new
                              {
                                  id = employes.Id,
                                  employeName = $"{employes.Lastname.Trim()} {employes.Firstname.Trim()} {employes.Patronymic.Trim()}",
                                  telegramId = employes.TelegramId
                              };
                //logger.LogInformation($"getdata: {getdata}");
                return getdata.ToList();
            });

            app.MapGet("/getmytrips/{telegramId}", (long telegramId) =>
            {
                var getData = from trips in context.Trips
                              join managers in context.Managers on trips.ManagerId equals managers.Id
                              join employes in context.Employes on trips.EmployeId equals employes.Id
                              join company in context.Companies on trips.CompanyId equals company.Id
                              join triptype in context.Triptypes on trips.TripTypeId equals triptype.Id
                              where telegramId == employes.TelegramId
                              select new
                              {
                                  id = trips.Id,
                                  managerName = $"{managers.Lastname.Trim()} {managers.Firstname.Trim()} {managers.Patronymic.Trim()}",
                                  employe = $"{employes.Lastname.Trim()} {employes.Firstname.Trim()} {employes.Patronymic.Trim()}",
                                  dateOfTrip = trips.TripDate.Value.ToShortDateString(),
                                  company = company.CompanyName.Trim(),
                                  deadLine = trips.DeadlineContract.Value.ToShortDateString(),
                                  customer = trips.Customer.Trim(),
                                  address = trips.CustomerAddress.Trim(),
                                  caption = trips.Caption.Trim()
                              };
                return getData.ToList();
            });

            app.MapPost("/asigntrip", (AsignTripToEmploye trip) =>
            {
                ILogger logger = loggerFactory.CreateLogger("AsingTrip");
                
                //logger.LogInformation($"Input status: {input}");
                //var trip = JsonSerializer.Deserialize<AsignTripToEmploye>(input);
                if (trip == null)
                {
                    return HttpStatusCode.NotFound;
                }
                else
                {
                    var editedTrip = context.Trips.FirstOrDefault(t => t.Id == trip.tripId);
                    editedTrip.TripDate = trip.dateTrip;
                    editedTrip.EmployeId = trip.employeId;
                    context.Trips.Update(editedTrip);
                    context.SaveChanges();
                }

                return HttpStatusCode.OK;
            });

            
            app.Run();
        }

    }
}