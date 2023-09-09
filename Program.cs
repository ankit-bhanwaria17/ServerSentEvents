using Microsoft.AspNetCore.Mvc;
using System.Text;
using Timer = System.Timers.Timer;

namespace ServerSentEvents
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddCors();
            var app = builder.Build();

            app.UseCors(policy =>
            {
                policy.AllowAnyOrigin()
                      .AllowAnyMethod()
                      .AllowAnyHeader();
            });

            app.MapGet("/stream", (HttpContext context) =>
            {
                context.Response.ContentType = "text/event-stream";
                Timer t = new Timer();
                t.Interval = 2000;
                t.AutoReset = true;
                t.Elapsed += (sender, e) =>
                {
                    context.Response.WriteAsync($"data: Streaming at {DateTime.Now.ToShortTimeString()}....\n\n");
                };
                t.Start();
                while (true) ;
            });

            app.Run();
        }
    }
}

