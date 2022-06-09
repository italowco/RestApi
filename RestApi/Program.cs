using Microsoft.EntityFrameworkCore;
using RestApi.Infraestructure.Data;
using RestApi.Service;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using System.Text;
using RestApi.Service.Interfaces;
using RequestLoggingMiddleware.Logging;
using Elmah.Io.Extensions.Logging;


var builder = WebApplication.CreateBuilder(args);


IConfiguration configuration = builder.Configuration;

if (builder.Environment.IsDevelopment())
{

    builder.Host.ConfigureLogging(logging => {
        logging.ClearProviders();
        
        logging.AddElmahIo(options =>
        {
            options.ApiKey = "603d3feaed0b45379cb3c1cbbbc26a5b";
            options.LogId = new Guid("4454945c-49c0-424d-9694-2b6ea0d209ce");
        });
        logging.AddFilter<ElmahIoLoggerProvider>(null, LogLevel.Information);
    });
}


builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Configuraçoes de banco de dados
builder.Services.AddDbContext<DataContext>(opt => opt.UseSqlServer(
    builder.Configuration.GetConnectionString("DefaultConnection")));

//builder.Services.AddScoped<DataContext, DataContext>();

builder.Services.AddIdentity<IdentityUser, IdentityRole>()
    .AddEntityFrameworkStores<DataContext>()
    .AddDefaultTokenProviders();

builder.Services.Configure<IdentityOptions>(options =>
{
                    // Password settings.
                options.Password.RequireDigit = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                options.Password.RequiredLength = 6;
                options.Password.RequiredUniqueChars = 1;
});


#region .:Serviço de Autenticação:.
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
})
// Adding Jwt Bearer
.AddJwtBearer(options =>
{
    options.SaveToken = true;
    options.RequireHttpsMetadata = false;
    options.TokenValidationParameters = new TokenValidationParameters()
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidAudience = configuration["JWT:ValidAudience"],
        ValidIssuer = configuration["JWT:ValidIssuer"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:Secret"]))
    };
});
#endregion


builder.Services.AddScoped<ITokenService, TokenService>();

builder.Services.AddScoped<IScopedService, ScopedService>();
builder.Services.AddTransient<ITransientService, TransientService>();
builder.Services.AddSingleton<ISingletonService, SingletonService>();

var app = builder.Build();

// Configure the HTTP request pipeline.

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

#region Custom Middlewares
//app.Use(async (context, next) =>
//{
//    // Do work that can write to the Response.
//    await next.Invoke();
//    // Do logging or other work that doesn't write to the Response.
//});

//app.Run(async context =>
//{
//    await context.Response.WriteAsJsonAsync( new { messsage = "Hello world" });

//});


//app.UseMiddleware<RequestLogging>(); 
#endregion

app.UseRouting();

app.MapControllers();

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();


app.Run();
