
using HotelPlatform.Services.Implementation;
using HotelPlatform.Services.Interfaces;
using HotelPlatform.Shared.DTOs.PaymentDTOs;

namespace HotelPlatform.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            // Inject the Services i want 
            builder.Services.AddScoped<IPaymentService, PaymentService>();

            // binding the options of Fawaterak in FawaterakOptions
            builder.Services.Configure<FawaterakOptions>(builder.Configuration.GetSection("Fawaterak"));
            
            // add the service to Get IHttpClientFactory
            builder.Services.AddHttpClient();
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
