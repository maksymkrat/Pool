using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Pool.API.Authentication;
using Pool.API.Repository;
using Pool.API.Repository.IRepository;
using Pool.API.Services;
using Pool.API.Services.IServicec;

var builder = WebApplication.CreateBuilder(args);

builder.Logging.ClearProviders();
builder.Logging.AddConsole();

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
// builder.Services.AddSwaggerGen(c =>
// {
//     c.SwaggerDoc("v1", new OpenApiInfo {Title = "Pool.API", Version = "v1"});
// });
builder.Services.AddHttpClient();

builder.Services.AddSingleton<IUserRepository,UserRepository>();
builder.Services.AddSingleton<IWordRepository,WordRepository>();

builder.Services.AddSingleton<IUserService, UserService>();
builder.Services.AddSingleton<IWordService, WordService>();
builder.Services.AddSingleton<ITranslatorService, TranslatorService>();


builder.Services.AddAuthentication(o =>
{
    o.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    o.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(o =>
{
    o.RequireHttpsMetadata = false;
    o.SaveToken = true;
    o.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(JwtAuthenticationManager.JWT_SECURITY_KEY )),
        ValidateIssuer = false,
        ValidateAudience = false
    };

});

var app = builder.Build();

app.MapGet("/", () => "api ran");

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    // app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI();
    // app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger", "Pool.API v1"));
}



app.UseAuthentication();
app.UseAuthorization();     

app.MapControllers();

app.UseCors(x => x
    .AllowAnyMethod()
    .AllowAnyHeader()
    .SetIsOriginAllowed(origin => true) 
    .AllowCredentials()); 

app.Run();