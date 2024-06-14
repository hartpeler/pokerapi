global using System;
global using Microsoft.EntityFrameworkCore;
global using th_poker_api.Data;
global using th_poker_api.Model.Game;
global using Microsoft.OpenApi.Models;
global using Swashbuckle.AspNetCore.Filters;
global using th_poker_api.Common;
global using System.ComponentModel.DataAnnotations;
global using th_poker_api.Model.Player;
global using th_poker_api.Model.Transactions;
global using Microsoft.EntityFrameworkCore.Migrations;
global using th_poker_api.Services.AuthService;
global using Microsoft.IdentityModel.Tokens;
global using th_poker_api.DTO.Player;
global using th_poker_api.Services.UserService;
global using th_poker_api.Services.RoomService;

using Microsoft.AspNetCore.Authentication.JwtBearer;
using th_poker_api.DTO.Email;
using th_poker_api.Services.EmailService;
using Microsoft.AspNetCore.Http.Features;
using Newtonsoft.Json;
using th_poker_api.Services.GameplayService;
using th_poker_api.Mapper;
using AutoMapper;
using th_poker_api.Services.SpinningWheelService;
using th_poker_api.Services.FriendsService;
using th_poker_api.Services.PurchaseService;
using th_poker_api.Services.HistoryService;

namespace th_poker_api;
public class Program
{
    public static void Main(string[] args)
    {
        DatabaseConnection dc = new DatabaseConnection();

        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.

        builder.Services.AddControllers();
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddControllers().AddNewtonsoftJson(x =>
            x.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore);
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen(options =>
        {
            options.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
            {
                Description = "Standard Authorization header using the Bearer scheme (\"bearer {token}\")",
                In = ParameterLocation.Header,
                Name = "Authorization",
                Type = SecuritySchemeType.ApiKey
            });

            options.OperationFilter<SecurityRequirementsOperationFilter>();
        });

        builder.Services.AddScoped<IAuthService, AuthService>();
        builder.Services.AddScoped<IUserService, UserService>();
        builder.Services.AddScoped<IRoomService, RoomService>();
        builder.Services.AddScoped<IGameplayService, GameplayService>();
        builder.Services.AddScoped<ISpinningWheel, SpinningWheel>();
        builder.Services.AddScoped<IFriendsService, FriendsService>();
        builder.Services.AddScoped<IPurchaseService, PurchaseService>();
        builder.Services.AddScoped<IHistoryService, HistoryService>();

        builder.Services.AddScoped<APIKey>();
        builder.Services.AddDbContext<DataContext>();

        var automapper = new MapperConfiguration(item => item.AddProfile(new AutoMapperProfile()));
        IMapper mapper = automapper.CreateMapper();
        builder.Services.AddSingleton(mapper);

        builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(System.Text.Encoding.UTF8
                        .GetBytes(builder.Configuration.GetSection("AppSettings:Token").Value)),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            });

        builder.Services.AddHttpContextAccessor();
        builder.Services.AddCors(options => options.AddPolicy(name: "NgOrigins",
            policy =>
            {
                policy.WithOrigins("https://localhost:7177").AllowAnyMethod().AllowAnyHeader();
            }));
        //tambahan
        var emailconfig = builder.Configuration.GetSection("EmailConfiguration").Get<EmailConfiguration>();
        builder.Services.AddSingleton(emailconfig);
        builder.Services.AddScoped<IEmailService, EmailService>();
        builder.Services.Configure<FormOptions>(o =>
        {
            o.ValueCountLimit = int.MaxValue;
            o.MultipartBodyLengthLimit = int.MaxValue;
            o.MemoryBufferThreshold = int.MaxValue;
        });
        //sini
        var app = builder.Build();

        // Configure the HTTP request pipeline.
        // kalau udah PROD harus diaktifkan lagi
        /*if (app.Environment.IsDevelopment())
          {
              app.UseSwagger();
              app.UseSwaggerUI();
          }*/
        app.UseSwagger();   // Harus hapus ketika PROD 
        app.UseSwaggerUI(); // Harus hapus ketika PROD 

        app.UseCors("NgOrigins");

        app.UseHttpsRedirection();

        app.UseAuthentication();

        app.UseAuthorization();

        app.MapControllers();
        app.UseHttpsRedirection();

        app.Run();
    }
}

