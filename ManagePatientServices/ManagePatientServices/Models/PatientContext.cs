using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using System.Reflection.Metadata;

namespace ManagePatientServices.Models
{
    public class PatientContext : DbContext
    {
        public DbSet<Patient> Patients { get; set; }
        public DbSet<Organization> Organizations { get; set; }
        public DbSet<PatientVisit> PatientVisits { get; set; }
        public DbSet<PatientProgressNotes> PatientProgressNotes { get; set; }
        public string DbPath { get; }

        public PatientContext()
        {
            var folder = Environment.SpecialFolder.LocalApplicationData;
            var path = Environment.GetFolderPath(folder);
            DbPath = System.IO.Path.Join(path, "patients.db");
        }
        protected override void OnConfiguring(DbContextOptionsBuilder options)
            => options.UseSqlite($"Data Source={DbPath}");
    }
}
