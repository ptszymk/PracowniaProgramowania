#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PracowniaProgramowania.Models;

namespace PracowniaProgramowania.Data
{
    public class PracowniaProgramowaniaContext : DbContext
    {
        public PracowniaProgramowaniaContext (DbContextOptions<PracowniaProgramowaniaContext> options)
            : base(options)
        {
        }

        public DbSet<PracowniaProgramowania.Models.Users> Users { get; set; }
        public DbSet<PracowniaProgramowania.Models.Roles> Roles { get; set; }
    }
}
