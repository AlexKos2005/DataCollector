using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Npgsql.EntityFrameworkCore;
using DataCollector.Entities;


namespace DataCollector.Servises.DAL
{
    public class PostgresDataContext : DbContext
    {
        private PgSQLConnectionSettings _settings = null;
        public DbSet<Dose>doses { get; set; }

        public PostgresDataContext(PgSQLConnectionSettings settings)
        {
            _settings = settings ?? throw new ArgumentNullException(nameof(settings));
            Database.EnsureCreated();
        }

        //public PostgresDataContext()
        //{
        //    Database.EnsureCreated();
        //}

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql($"Host={_settings.HostName};Port={_settings.PortAddress};Database={_settings.DataBaseName};Username={_settings.UserName};Password={_settings.Password}");
            //optionsBuilder.UseNpgsql("Host=10.87.20.183;Port=5432;Database=APCS1;Username=apcs_admin;Password=qwerty");
            //optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=TestDB;Username=postgres;Password=postgres");

        }
    }
}
