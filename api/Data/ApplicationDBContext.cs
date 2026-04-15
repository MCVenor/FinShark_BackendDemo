using api.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;

namespace api.Data
{
    public class ApplicationDBContext : IdentityDbContext<AppUser>
    {
        public ApplicationDBContext(DbContextOptions dbContextOptions)
        : base(dbContextOptions)
        {

        }

        // Define your tables here
        public DbSet<Stock> Stocks { get; set; }
        // Define your tables here
        public DbSet<Comment> Comments { get; set; }


        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            // Generate these once and never change them!
            string adminRoleId = "BCD2FAEA-15E4-4796-B135-33024F72EFED";
            string userRoleId = "5CBAF4A9-D8FB-425F-9D4E-411F84AF0D1F";

            List<IdentityRole> roles = new List<IdentityRole>()
            {
                new IdentityRole
                {
                    Id = adminRoleId,
                    Name = "Admin",
                    NormalizedName = "ADMIN",
                     ConcurrencyStamp = "E8A26399-BCDC-4DFB-8862-97E468E779A1"

                },
                new IdentityRole
                {
                    Id = userRoleId,
                    Name = "User",
                    NormalizedName = "USER",
                    ConcurrencyStamp = "C8736D83-9478-489D-8082-9C16BB58406D"
                }
            };
            builder.Entity<IdentityRole>().HasData(roles);
        }
    }
}