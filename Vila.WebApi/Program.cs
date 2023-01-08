using Microsoft.EntityFrameworkCore;
using Vila.WebApi.Context;
using Vila.WebApi.Mappings;
using Vila.WebApi.Services.Vila;
using Vila.WebApi.Services.Detail;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.SwaggerGen;
using Vila.WebApi.Utility;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.AspNetCore.Mvc.Versioning;
using Vila.WebApi.Services.Customer;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);
var services = builder.Services;
var local = builder.Configuration.GetConnectionString("Local");
services.AddCors();
services.AddControllers();
services.AddDbContext<DataContext>(x =>
{
    x.UseSqlServer(local);
});
#region Dependency
services.AddTransient<IVilaService, VilaService>();
services.AddTransient<IDetailService, DetailService>();
services.AddTransient<ICustomerService, CustomerService>();
#endregion
#region AutoMapper
services.AddAutoMapper(typeof(ModelsMapper));
#endregion
#region Versioning
services.AddApiVersioning(option =>
{
    option.DefaultApiVersion = new Microsoft.AspNetCore.Mvc.ApiVersion(1, 0);
    option.AssumeDefaultVersionWhenUnspecified = true;
    option.ReportApiVersions = true;
    //option.ApiVersionReader = new HeaderApiVersionReader("X-ApiVersion");
});

services.AddVersionedApiExplorer(x =>
{
    x.GroupNameFormat = "'v'VVVV";
});
#endregion
#region Swagger
services.AddTransient<IConfigureOptions<SwaggerGenOptions>, SwaggerVilaDocument>();
services.AddSwaggerGen();
#endregion
#region Jwt
var JwtSettingSection = builder.Configuration.GetSection("JWTSettings");
services.Configure<JWTSettings>(JwtSettingSection);

var jwtsetting = JwtSettingSection.Get<JWTSettings>();

var key = Encoding.ASCII.GetBytes(jwtsetting.Secret);


services.AddAuthentication(x =>
{
    x.DefaultSignInScheme = JwtBearerDefaults.AuthenticationScheme;
    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(option =>
{
    option.TokenValidationParameters = new TokenValidationParameters
    {
        IssuerSigningKey = new SymmetricSecurityKey(key),
        ValidateIssuerSigningKey = true,
        ValidIssuer = jwtsetting.Issuer,
        ValidateIssuer = true,
        ValidAudience = jwtsetting.Audience,
        ValidateAudience=true,
        ValidateLifetime = true
    };
});
#endregion
var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(x =>
    {
        var provider = app.Services.CreateScope().ServiceProvider
        .GetRequiredService<IApiVersionDescriptionProvider>();

        foreach (var item in provider.ApiVersionDescriptions)
        {
            x.SwaggerEndpoint($"/swagger/{item.GroupName}/swagger.json", item.GroupName.ToString());
        }

        //x.SwaggerEndpoint("/swagger/VilaOpenApi/swagger.json", " Vila Open Api");
        x.RoutePrefix = "";
    });
}

app.UseHttpsRedirection();
app.UseCors(x =>
x.AllowAnyOrigin().
AllowAnyHeader().
AllowAnyMethod()
);
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
