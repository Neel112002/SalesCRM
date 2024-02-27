using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Models;



namespace WebApplication1.Data
{
    // Inherit from IdentityDbContext<ApplicationUser> to include Identity functionality
    public class ApplicationDbContext : IdentityDbContext

    {
        // Constructor
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        // DbSet for SalesLeadEntity
        public DbSet<SalesLeadEntity> SalesLeads { get; set; }
    }
}
