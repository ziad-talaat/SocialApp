using Infrastructure.ServicesDependencies;
using Serilog;
namespace SocialApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddInfrastructure(builder.Configuration);
            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();


            //Serilog

            builder.Host.UseSerilog((HostBuilderContext context, IServiceProvider services
                , LoggerConfiguration loggerConfiguration) =>
            {
                loggerConfiguration.ReadFrom.Configuration(context.Configuration)  //context .configuration means appsettingFie and environment ,  Read Serilog's settings from my application's configuration."
                .ReadFrom.Services(services);//read current app's services and make them available to serilog
            });
              

            //built in logging

            //builder.Host.ConfigureLogging(loggingProvider => {
            //    loggingProvider.ClearProviders();
            //    loggingProvider.AddConsole();
            //});
            


            var app = builder.Build();
            app.UseSerilogRequestLogging();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            //app.UseHttpLogging();

            app.UseRequestLocalization();
            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}
