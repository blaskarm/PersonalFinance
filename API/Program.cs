using API.Extensions;
using API.OptionsSetup;
using Application;
using Infrastructure;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Options;

namespace API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);


            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGenWithAuth();

            builder.Services.ConfigureOptions<JwtOptionsSetup>();
            //builder.Services.ConfigureOptions<JwtBearerOptionsSetup>();

            builder.Services
                .AddInfrastructureLayer(builder.Configuration)
                .AddApplicationLayer();

            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthentication();
            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
