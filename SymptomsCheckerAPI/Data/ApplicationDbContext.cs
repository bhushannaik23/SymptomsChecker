using Microsoft.EntityFrameworkCore;
using Symptoms_Checker.Models;

namespace Symptoms_Checker.Data{
    public class ApplicationDbContext:DbContext{

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options):base(options){}

        public DbSet<Patient> Patients{get;set;}
        public DbSet<Doctor> Doctors{get;set;}
        public DbSet<Admin> Admins{get;set;}
        public DbSet<Specialty> Specialties{get;set;}
        public DbSet<DoctorSpecialty> DoctorSpecialties{get;set;}
        public DbSet<Appointment> Appointments{get;set;}
        public DbSet<PatientHistory> PatientHistories{get;set;}
        public DbSet<SymptomInput> SymptomInputs{get;set;}  

    }
}