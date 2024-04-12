
using FirstChat.Db;
using FirstChat.Hubs;
using Microsoft.AspNetCore.Mvc.ApplicationParts;

namespace FirstChat
{
    public class Program
    {
        public static void Main(string[] args)
        {
            // Настройка ADI-контейнера
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddSignalR();
            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddCors(opt =>
            {
                opt.AddPolicy("reactApp", builder =>
                {
                    builder.WithOrigins("http://localhost:3000").AllowAnyHeader().AllowAnyMethod().AllowCredentials();
                });
            });

            builder.Services.AddSingleton<DbConnect>();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection(); // Перенаправление запроса

            app.UseAuthorization(); // Авторизация

            app.MapControllers(); // Контроллер

            app.MapHub<ChatHub>("/chat");

            app.UseCors("reactApp");

            app.Run();
        }
    }
}
