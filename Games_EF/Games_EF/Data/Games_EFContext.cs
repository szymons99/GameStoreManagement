using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Games__MVN_EF_Razor.Models.GameStore;
using Games_EF.Models.Developers;
using Games_EF.Models.Awards;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace Games_EF.Data
{
    public class Games_EFContext : IdentityDbContext
    {
        public Games_EFContext (DbContextOptions<Games_EFContext> options)
            : base(options)
        {
        }

        public DbSet<GameModel> Games { get; set; }
        public DbSet<DeveloperModel> Developers { get; set; }
        public DbSet<AwardModel> Awards { get; set; }
    }
}
