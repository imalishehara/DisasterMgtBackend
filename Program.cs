using Disaster_demo.Models;
using Disaster_demo.Services;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;
using System.Text.Json.Serialization;

var MyAllowSpecificOrigins = "*";

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<DisasterDBContext>(options =>
{
    options.UseMySQL(builder.Configuration.GetConnectionString("defaultDB"));
    //options.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
});

builder.Services.AddScoped<ISymptomsServices, SymptomsServices>();

builder.Services.AddScoped<IAidRequestServices, AidRequestServices>();

builder.Services.AddScoped<AlertServices>();

builder.Services.AddScoped<IVolunteerServices, VolunteerServices>();

builder.Services.AddScoped<IUserServices, UserServices>();

builder.Services.AddScoped<IDSOfficerServices, DSOfficerServices>();

builder.Services.AddScoped<IDMCOfficerServices, DMCOfficerServices>();

builder.Services.AddScoped<IStatisticsService, StatisticsService>();

builder.Services.AddScoped<IContributionService, ContributionService>();






//builder.Services.AddScoped<IAlertServices, AlertServices>();


//builder.Services.AddCors(options =>
//{
//    options.AddPolicy(MyAllowSpecificOrigins,policy
//                          =>
//                          {
//                              //policy.WithOrigins("*")
//                                                 .AllowAnyOrigin()
//                                                  .AllowAnyHeader()
//                                                  .AllowAnyMethod();
//                          });
//});


builder.Services.AddCors(options =>
{
    options.AddPolicy(name: MyAllowSpecificOrigins, policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.PropertyNameCaseInsensitive = true;
        options.JsonSerializerOptions.Converters.Add(
            new JsonStringEnumConverter(JsonNamingPolicy.CamelCase));
    });


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseStaticFiles();



app.UseCors(MyAllowSpecificOrigins);

app.UseRouting();



app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
