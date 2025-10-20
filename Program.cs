using System.Text.Json.Serialization;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using PolyclinicRegistryOffice.Application.Handlers.ReadHandlers.ScheduleSlot.Read;
using PolyclinicRegistryOffice.Application.Handlers.ReadHandlers.Specialist.Read;
using PolyclinicRegistryOffice.Application.Handlers.ReadHandlers.Specialization.Read;
using PolyclinicRegistryOffice.Application.Handlers.WriteHandlers.Appointment.Write;
using PolyclinicRegistryOffice.Application.Handlers.WriteHandlers.Patient.Write;
using PolyclinicRegistryOffice.Application.Handlers.WriteHandlers.ScheduleSlot.Write;
using PolyclinicRegistryOffice.Application.Handlers.WriteHandlers.Specialist.Write;
using PolyclinicRegistryOffice.Application.Handlers.WriteHandlers.Specialization.Write;
using PolyclinicRegistryOffice.Application.Interfaces.ReadInterfaces;
using PolyclinicRegistryOffice.Application.Interfaces.WriteInterfaces;
using PolyclinicRegistryOffice.Data;
using PolyclinicRegistryOffice.Interfaces;
using PolyclinicRegistryOffice.Validators;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<WriteDbContext>(options => options.UseNpgsql(connectionString));
builder.Services.AddTransient<ISpecializationWriteHandler, SpecializationWriteHandler>();
builder.Services.AddTransient<ISpecialistWriteHandler, SpecialistWriteHandler>();
builder.Services.AddTransient<IPatientWriteHandler, PatientWriteHandler>();
builder.Services.AddTransient<IAppointmentWriteHandler, AppointmentWriteHandler>();
builder.Services.AddTransient<IScheduleSlotWriteHandler, ScheduleSlotWriteHandler>();
builder.Services.AddTransient<ISpecialistReadHandler, SpecialistReadHandler>();
builder.Services.AddTransient<ISpecializationReadHandler, SpecializationReadHandler>();
builder.Services.AddTransient<IScheduleSlotReadHandler, ScheduleSlotReadHandler>();

builder.Services.AddSingleton<ISqlConnectionFactory>(new SqlConnectionFactory(connectionString!));

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

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend",
        policy =>
        {
            policy.WithOrigins("http://localhost:5173") 
                .AllowAnyHeader()
                .AllowAnyMethod()
                .AllowCredentials();
        });
});

builder.Services.AddSwaggerGen();
builder.Services.AddOpenApi();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try
    {
        var db = services.GetRequiredService<WriteDbContext>();
        db.Database.Migrate();

        var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();
        var userManager = services.GetRequiredService<UserManager<IdentityUser>>();

        if (!await roleManager.RoleExistsAsync("Admin"))
            await roleManager.CreateAsync(new IdentityRole("Admin"));

        var adminEmail = "admin@example.com";
        var adminPassword = "P@ssw0rd123";

        var admin = await userManager.FindByEmailAsync(adminEmail);
        if (admin == null)
        {
            admin = new IdentityUser { UserName = adminEmail, Email = adminEmail, EmailConfirmed = true };
            var res = await userManager.CreateAsync(admin, adminPassword);
            if (res.Succeeded)
                await userManager.AddToRoleAsync(admin, "Admin");
        }
    }
    catch (Exception ex)
    {
        Console.WriteLine(ex);
    }
}

app.UseHttpsRedirection();

app.UseRouting();
app.UseCors("AllowFrontend");
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();

