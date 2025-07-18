using Disaster_demo.Models.Entities;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System.Numerics;

namespace Disaster_demo.Models
{
    public class DisasterDBContext :DbContext
    {
        //private DbSet<GN_Officer> Gn_officer;
        private DbSet<Symptoms> symptoms;

        public DisasterDBContext(DbContextOptions<DisasterDBContext> options) : base(options)
        {
        }

        //public DbSet<GN_Officer> GN_Officer{ get => Gn_officer; set => Gn_officer = value; }
        public DbSet<Symptoms> Symptoms { get => symptoms; set => symptoms = value; }

        public DbSet<AidRequests> AidRequests { get; set; }

        public DbSet<Alerts> Alerts { get; set; }

        public DbSet<Users> Users { get; set; }

        public DbSet<Volunteer> Volunteers { get; set; }
        public DbSet<DS_Officer> DS_Officers { get; set; }
        //public DbSet<DMCOfficer> DMC_Officers { get; set; }
        public DbSet<DMCOfficer> DMCOfficers { get; set; }

        public DbSet<Contribution> Contribution { get; set; }





        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Convert GnApprove enum to string
            modelBuilder.Entity<AidRequests>()
                .Property(a => a.dsApprove)
                .HasConversion<string>();

            // Convert DmcApprove enum to string
            modelBuilder.Entity<AidRequests>()
                .Property(a => a.dmcApprove)
                .HasConversion<string>();

            modelBuilder.Entity<Alerts>()
            .Property(a => a.severity)
            .HasConversion(new EnumToStringConverter<SeverityLevel>());

            modelBuilder.Entity<Alerts>()
                .Property(a => a.status)
                .HasConversion(new EnumToStringConverter<AlertStatus>());


            modelBuilder.Entity<Users>().ToTable("Users");

            // Map derived type to Volunteer table (TPT)
            //modelBuilder.Entity<Volunteer>().ToTable("Volunteer");

            modelBuilder.Entity<Volunteer>().ToTable("volunteer");

            modelBuilder.Entity<Users>()
           .Property(u => u.role)
           .HasConversion<string>();

            modelBuilder.Entity<Users>()
           .Property(u => u.status)
           .HasConversion<string>();


            modelBuilder.Entity<Volunteer>()
        .Property(v => v.availability)
        .HasConversion<string>();

            modelBuilder.Entity<AidRequests>()
            .Property(e => e.request_type)
           .HasConversion<string>();

            //modelBuilder.Entity<GN_Officer>().ToTable("GN_Officer");

            modelBuilder.Entity<DMCOfficer>().ToTable("dmc_officers");

            modelBuilder.Entity<DMCOfficer>()
        .ToTable("dmc_officers")
        .HasBaseType<Users>();

            modelBuilder.Entity<Volunteer>().HasBaseType<Users>();


            modelBuilder.Entity<Contribution>()
        .ToTable("contributions");

            



        }
    }


}
