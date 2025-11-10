using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.Text;
using Symptoms_Checker.Data;
using Symptoms_Checker.Repositories;
using Symptoms_Checker.Services;


var builder = WebApplication.CreateBuilder(args);

// ----------------------------------------------------------------------
//  Database
// ----------------------------------------------------------------------
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))
);

// ----------------------------------------------------------------------
//  Repositories
// ----------------------------------------------------------------------
builder.Services.AddScoped<IPatientRepository, PatientRepository>();
builder.Services.AddScoped<IDoctorRepository, DoctorRepository>();
builder.Services.AddScoped<IAppointmentRepository, AppointmentRepository>();
builder.Services.AddScoped<IPatientHistoryRepository, PatientHistoryRepository>();
builder.Services.AddScoped<ISymptomInputRepository, SymptomInputRepository>();

// ----------------------------------------------------------------------
//  Services
// ----------------------------------------------------------------------
builder.Services.AddScoped<IPatientService, PatientService>();
builder.Services.AddScoped<IDoctorService, DoctorService>();
builder.Services.AddScoped<IAppointmentService, AppointmentService>();
builder.Services.AddScoped<ISymptomAnalysisService, SymptomAnalysisService>();
builder.Services.AddScoped<IGeminiService, GeminiService>();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IJwtService, JwtService>();
builder.Services.AddHttpClient();

// ----------------------------------------------------------------------
//  JWT Authentication
// ----------------------------------------------------------------------
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,

        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidAudience = builder.Configuration["Jwt:Audience"],

        IssuerSigningKey =
            new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"])
            ),

        RoleClaimType = ClaimTypes.Role
    };
});

// ----------------------------------------------------------------------
//  Authorization Policies
// ----------------------------------------------------------------------
builder.Services.AddAuthorizationBuilder()
    .AddPolicy("AdminOnly", policy => policy.RequireRole("admin"))
    .AddPolicy("DoctorOnly", policy => policy.RequireRole("doctor"))
    .AddPolicy("PatientOnly", policy => policy.RequireRole("patient"));

// ----------------------------------------------------------------------
// Controllers
// ----------------------------------------------------------------------
builder.Services.AddControllers();

// ----------------------------------------------------------------------
// CORS
// ----------------------------------------------------------------------
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAngular", policy =>
        policy.WithOrigins("http://localhost:4200")
              .AllowAnyHeader()
              .AllowAnyMethod()
    );
});

// ----------------------------------------------------------------------
// Swagger
// ----------------------------------------------------------------------
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// ----------------------------------------------------------------------
// Build App
// ----------------------------------------------------------------------
var app = builder.Build();


// ----------------------------------------------------------------------
// Middleware Pipeline
// ----------------------------------------------------------------------
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("AllowAngular");

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
