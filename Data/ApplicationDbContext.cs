using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using stockApi.Models;

namespace stockApi.Data
{
    public class ApplicationDbContext:IdentityDbContext<AppUser>
    {
        public ApplicationDbContext(DbContextOptions dbContextOptions):base(dbContextOptions)
        {
          
            
        }
        public DbSet<Stock>Stocks {get;set;}= null!;
        public DbSet<Comment> Comments {get;set;} = null!;
    }
}