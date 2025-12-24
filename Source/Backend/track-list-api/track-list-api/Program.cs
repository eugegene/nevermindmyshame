using System.Globalization;
using System.IO.Compression;
using api.DbContext;
using dotenv.net;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.IdentityModel.Logging;
using static api.Configure;
using AutoMapper;
using api.Repository.IReposotory;
using api.Repository;
using FluentValidation;

namespace api;

public static class Program
{
    public static void Main(string[] args)
    {
        CultureInfo.DefaultThreadCurrentCulture = CultureInfo.InvariantCulture;
        CultureInfo.DefaultThreadCurrentUICulture = CultureInfo.InvariantCulture;

        IdentityModelEventSource.ShowPII = true;

        AddUkrainianLanguageSupport();

        DotEnv.Load(new DotEnvOptions(false, trimValues: true));

        CreateRootDirectoryIfNotExists();

        var builder = WebApplication.CreateBuilder(args);

        builder.Configuration.AddJsonFile("appsettings.json", true, true);

        builder.Services.AddValidatorsFromAssemblies(AppDomain.CurrentDomain.GetAssemblies());

        AddIfDevelopmentSuppressModelStateInvalidFilter(builder);

        ConfigControllers<TrackListDbContext>(builder, "CONNECTION_STRING");
        ConfigureNewtonJson();

        AddLogs(builder);

        builder.Services.AddCors(options =>
        {
            options.AddPolicy("DefaultCors", policy =>
            {
                policy.AllowAnyHeader()
                    .AllowAnyMethod()
                    .SetIsOriginAllowed(_ => true) // Allows all origins *correctly*
                    .AllowCredentials(); // Only if you need cookies / auth
            });
        });

        AddCompression(builder, CompressionLevel.Optimal);

        AddSwagger(builder);

        AddAuthenticationAndAuthorisation(builder);

        builder.Services.Configure<FormOptions>(options => { options.MultipartBodyLengthLimit = 10000000; });

        builder.Services.AddAutoMapper(cfg =>
        {
            /* configuration */
        }, AppDomain.CurrentDomain.GetAssemblies());

        builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

        var app = builder.Build();

        app.UseHttpLogging();

        app.UseStaticFiles();

        IfIsDevelopmentUseSwaggerElseHsts(app);

        if (app.Environment.IsDevelopment()) IdentityModelEventSource.ShowPII = true;

        app.UseHttpsRedirection();
        app.UseStaticFiles();

        app.UseRouting();
        app.UseCors("DefaultCors");

        app.UseAuthentication();
        app.UseAuthorization();

        app.MapControllers();

        app.Run();
    }
}