using JwtAuthentication.Api.Helpers;
using JwtAuthentication.Infrastructure.Repositories.Interface;
using JwtAuthentication.Infrastructure.Repositories;
using JwtAuthentication.Infrastructure.Services.Interface;
using JwtAuthentication.Infrastructure.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//JWT Authentication settings Step 2 
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration["JwtToken:Issuer"],
        ValidAudience = builder.Configuration["JwtToken:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JwtToken:Key"] ?? throw new ArgumentNullException("Invalid key")))
    };
});

//builder.Services.AddAuthorization(); //Step 7 
//Application Specific Settings
ConfigureApplicationSpecificSettings(builder);

builder.Services.AddAuthorization();

//Adding the services to the DIcontainer
builder.Services.AddScoped<ICustomAuthenticationService, CustomAuthenticationService>();
builder.Services.AddScoped<IUserRepository, UserRepository>();

var app = builder.Build();

//configure the authentication middleware
app.UseAuthentication();  //Step5: 

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

static void ConfigureApplicationSpecificSettings(WebApplicationBuilder builder)
{

    // configure the GeoUtils class AllowedDistanceInFeet property
    GeoUtils.AllowedDistanceInFeet = Convert.ToInt32(builder.Configuration["Location:ConfiguredLocationInFeet"]);
}