using System;
using System.Reflection;
using Api_Auth.Entity;
using Microsoft.EntityFrameworkCore;

namespace Api_Auth.Context
{
	public class AppDbContext : DbContext
    {
		public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
		}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);           
         
        }


        public DbSet<Empleado> Empleados { get; set; }
    }
}

