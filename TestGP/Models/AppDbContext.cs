using ImageHandling.Models;
using Microsoft.EntityFrameworkCore;
using PasswordHandlling.Models;
using System.Reflection;

namespace TestGP.Models
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {


        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }

        // public DbSet<SuperAdminCompany> SuperAdminCompanies { get; set; }
        //public DbSet<Manager> Managers { get; set; }
        public DbSet<PasswordResetToken> passwordResetTokens { get; set; }

        public DbSet<Admin> Admins { get; set; }
        public DbSet<Driving> Drivings { get; set; }
        public DbSet<Company> Companies { get; set; }
        public DbSet<CurrentLocation> currentLocations { get; set; }
        public DbSet<TestLocation> TestLocation { get; set; }

        public DbSet<ReportData> reportData{ get; set; }
        public DbSet<Violation> Violations  { get; set; }
        //public DbSet<Applicant> Applicants { get; set; }
        //public DbSet<ApplicantCertificate> ApplicantCertificates { get; set; }//
        
        public DbSet<AssCommunicateAdmin> AssCommunicateAdmins { get; set; }
        public DbSet<Assistant> Assistants { get; set; }
        public DbSet<Car> Cars { get; set; }
       
        
        public DbSet<Driver> Drivers { get; set; }
        public DbSet<DriverCommunicateAss> DriverCommunicateAsses { get; set; }
       
        //public DbSet<Location> Locations{ get; set; }
        public DbSet<Manager> Managers{ get; set; }
        public DbSet<ManageViolation> ManageViolations{ get; set; }
        public DbSet<SuperAdminCompany> SuperAdminCompany{ get; set; }
        public DbSet<SuperAdminTreateManager> SuperAdminTreateManagers{ get; set; }
        public DbSet<carMaintenance> carMaintenances{ get; set; }
        public DbSet<carObd2Violation> carObd2Violations{ get; set; }
        public DbSet<DriverTest> DriverTest { get; set; }










    }
}