using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PolyclinicRegistryOffice.Entities;

namespace PolyclinicRegistryOffice.Data;

public class WriteDbContext : IdentityDbContext<IdentityUser>
{
    public WriteDbContext(DbContextOptions<WriteDbContext> options) : base(options){}
    
    public DbSet<Specialist> Specialists =>  Set<Specialist>();
    public DbSet<Feedback> Feedbacks =>  Set<Feedback>();
    public DbSet<Specialization> Specializations =>  Set<Specialization>();
    public DbSet<Patient> Patients => Set<Patient>();
    public DbSet<Appointment> Appointments => Set<Appointment>();
    public DbSet<ScheduleSlot> ScheduleSlots => Set<ScheduleSlot>();
}
