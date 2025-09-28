using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechnicalTask.BusinessLogic.Entities.Buiseness;
using TechnicalTask.BusinessLogic.Entities.General;

namespace TechnicalTask.DataAccess.Data
{
    public class ApplicationDBContext : IdentityDbContext<ApplicationUser, IdentityRole<int>, int>
    {


        public virtual DbSet<Customer> Customers { get; set; }
        public virtual DbSet<Order> Orders { get; set; }



        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options) : base(options)
        {
        }

    }
}
