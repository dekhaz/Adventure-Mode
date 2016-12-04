using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace AdventureMode
{
    class GameContext : DbContext
    {
        public DbSet<Encounter> Encounters { get; set; }
        public DbSet<Foe> Foes { get; set; }
        public DbSet<Item> Items { get; set; }
        public DbSet<Location> Locations { get; set; }
        public DbSet<Town> Towns { get; set;}
        public DbSet<Magic> Spells { get; set; }
        public DbSet<Hero> Characters { get; set; }

    }
}
