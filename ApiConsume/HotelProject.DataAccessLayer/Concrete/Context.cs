using HotelProject.EntityLayer.Concrete;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelProject.DataAccessLayer.Concrete
{
    public class Context:DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) //overide on yaz gelir gerisi
        {
            optionsBuilder.UseSqlServer("server=DESKTOP-PBFD0LU;initial catalog=OtelApiDb;integrated security=true; TrustServerCertificate=true");
            base.OnConfiguring(optionsBuilder);
        }

        public DbSet<Room> Rooms  { get; set; }
        public DbSet<Service> Services  { get; set; }
        public DbSet<Staff>  Staff { get; set; }
        public DbSet<Subscribe> Subscribes { get; set; }
        public DbSet<Testimonial> Testimonials { get; set; }
    }
}
