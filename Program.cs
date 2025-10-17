using System.Text.Json.Serialization;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using PolyclinicRegistryOffice.Application.Handlers.Appointment.Write;
using PolyclinicRegistryOffice.Application.Handlers.Patient.Write;
using PolyclinicRegistryOffice.Application.Handlers.ScheduleSlot.Write;
using PolyclinicRegistryOffice.Application.Handlers.Specialist.Write;
using PolyclinicRegistryOffice.Application.Handlers.Specialization.Write;
using PolyclinicRegistryOffice.Application.Interfaces;
using PolyclinicRegistryOffice.Data;
using PolyclinicRegistryOffice.Validators;

var builder = WebApplication.CreateBuilder(args);


var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<WriteDbContext>(options => options.UseNpgsql(connectionString));
builder.Services.AddTransient<ISpecializationWriteHandler, SpecializationWriteHandler>();
builder.Services.AddTransient<ISpecialistWriteHandler, SpecialistWriteHandler>();
builder.Services.AddTransient<IPatientWriteHandler, PatientWriteHandler>();
builder.Services.AddTransient<IAppointmentWriteHandler, AppointmentWriteHandler>();
builder.Services.AddTransient<IScheduleSlotWriteHandler, ScheduleSlotWriteHandler>();

builder.Services.AddControllersWithViews()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
    });

builder.Services.AddIdentity<IdentityUser, IdentityRole>(options =>
    {
        options.Password.RequireDigit = false;
        options.Password.RequireLowercase = false;
        options.Password.RequireNonAlphanumeric = false;
        options.Password.RequiredLength = 6;
    })
    .AddEntityFrameworkStores<WriteDbContext>()
    .AddDefaultTokenProviders();

builder.Services.AddControllers();

builder.Services.AddValidatorsFromAssemblyContaining<SpecializationValidator>();
builder.Services.AddValidatorsFromAssemblyContaining<SpecialistValidator>();
builder.Services.AddValidatorsFromAssemblyContaining<PatientValidator>();
builder.Services.AddValidatorsFromAssemblyContaining<ScheduleSlotValidator>();
builder.Services.AddValidatorsFromAssemblyContaining<AppointmentValidator>();
builder.Services.AddValidatorsFromAssemblyContaining<FeedbackValidator>();
builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddFluentValidationClientsideAdapters();

builder.Services.AddSwaggerGen();
builder.Services.AddOpenApi();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();

