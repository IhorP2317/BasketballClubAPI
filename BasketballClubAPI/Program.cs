using Azure.Core;
using BasketballClubAPI.Data;
using BasketballClubAPI.Interfaces;
using BasketballClubAPI.Repositories;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

    builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();

    builder.Services.AddScoped<IPlayerRepository, PlayerRepository>();
    builder.Services.AddScoped<ICoachRepository, CoachRepository>();
    builder.Services.AddScoped<ITeamRepository, TeamRepository>();
    builder.Services.AddScoped<IMatchRepository, MatchRepository>();
    builder.Services.AddScoped<IStatisticRepository, StatisticRepository>();
    builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
//Add data context to the container
builder.Services.AddDbContext<DataContext>(options =>
    {
        options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
    });

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment()) {
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseExceptionHandler(options => {
    options.Run(async context => {
        context.Response.StatusCode = StatusCodes.Status500InternalServerError;
        context.Response.ContentType = ContentType.ApplicationJson.ToString();

        var contextFeature = context.Features.Get<IExceptionHandlerFeature>();
        if (contextFeature != null) {
            await context.Response.WriteAsJsonAsync(new {
                StatusCode = context.Response.StatusCode,
                Message = contextFeature.Error.Message
            });
        }
    });
});
app.UseHttpsRedirection();

app.UseAuthorization();
app.UseCors("AllowAny");

app.MapControllers();

app.Run();
