//1. Usings to work with EntityFramework
using Serilog;
using UniversityApiBackend;
using Microsoft.OpenApi.Models;
using Microsoft.EntityFrameworkCore;
using UniversityApiBackend.DataAccess;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

//10. configure Serilog
builder.Host.UseSerilog((hostBuilderCtx, loggerConf) =>
{
    loggerConf
        .WriteTo.Console()
        .WriteTo.Debug()
        .ReadFrom.Configuration(hostBuilderCtx.Configuration);
});

//2. Connection with SQL Server Express
const string CONNECTIONNAME = "UniversityDB";
string connectionString = builder.Configuration.GetConnectionString(CONNECTIONNAME);

//builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

//3. Add Context
builder.Services.AddDbContext<UniversityDBContex>(options => options.UseSqlServer(connectionString));

//7.Add Service of JWT Authorization
builder.Services.AddJwtTokenServices(builder.Configuration);

// Add services to the container.
builder.Services.AddControllers();

//1- LOCALIZATION
builder.Services.AddLocalization(options => options.ResourcesPath = "Resources");

//8.Add Autorization
builder.Services.AddAuthorization(options =>
    {
        options.AddPolicy("UserOnlyPolicy", policy => policy.RequireClaim("UserOnly", "User 1"));
    });

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

//.9 TODO: Cpnfig Swagger to take care of Authorization of JWT
builder.Services.AddSwaggerGen(options =>
    {
        //We define the security for authorization
        options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
        {
            Name = "Authorization",
            Type = SecuritySchemeType.Http,
            Scheme = "Bearer",
            BearerFormat = "JWT",
            In = ParameterLocation.Header,
            Description = "JWT Authorization Header using Bearer Schema"
        });

        options.AddSecurityRequirement(new OpenApiSecurityRequirement
        {
            {
                new OpenApiSecurityScheme
                {
                    Reference = new OpenApiReference
                    {
                        Type= ReferenceType.SecurityScheme,
                        Id= "Bearer"
                    }
                }, new string[]{}
            }
        });
    });

//5. CORS configuration
builder.Services.AddCors(options =>
    options.AddPolicy(name: "CorsPolicy", builder =>
    {
        builder.AllowAnyOrigin();
        builder.AllowAnyMethod();
        builder.AllowAnyHeader();
    })
);

WebApplication app = builder.Build();

//2- SUPPORTED CULTURE
string[] supportedCultures = new[] { "en-US", "es-ES", "fr-FR", "de-DE" };
RequestLocalizationOptions localizationOptions = new RequestLocalizationOptions()
    .SetDefaultCulture(supportedCultures[1]) // English by default
    .AddSupportedCultures(supportedCultures) // Add all supported culture
    .AddSupportedUICultures(supportedCultures); // Add supported culture to UI

//3- Add localization to app
app.UseRequestLocalization(localizationOptions);

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//11. Tell app to use Serilog
app.UseSerilogRequestLogging();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

//6. Tell app to use CORS
app.UseCors("CorsPolicy");

app.Run();
