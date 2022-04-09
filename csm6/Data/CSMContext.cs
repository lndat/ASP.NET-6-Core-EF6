using csm6.Models;
using Microsoft.EntityFrameworkCore.SqlServer;
using Microsoft.EntityFrameworkCore;

namespace csm6.Data
{
    public class CSMContext : DbContext
    {
        public CSMContext(DbContextOptions<CSMContext> options) : base(options)
        {

        }

        public CSMContext()
        {

        }

        public DbSet<Player> Players { get; set; }
        public DbSet<Member> Members { get; set; }
        public DbSet<Team> Teams { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=(localdb)\MSSQLLocalDB;Database=CSMDb");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            #region Seed

            modelBuilder.Entity<Member>().HasData(new Member
            {
                Id = 1,
                Email = "luck@luck.at",
                FirstName = "Johannes",
                LastName = "Bamberger",
                PasswordHash = "kommtnoch",
                PasswordSalt = "kommtauch"
            });

            modelBuilder.Entity<Team>().HasData(new Team
            {
                Id = 1,
                LeagueId = 1,
                MemberId = 1,
                TeamName = "SK"
            });

            modelBuilder.Entity<League>().HasData(new League
            {
                Id = 1,
                LeagueName = "Bundesliga 1",
            });

            modelBuilder.Entity<Player>().HasData(
                new Player
                {
                    Id = 1,
                    Age = 18,
                    Aim = 10,
                    Experience = 10,
                    PlayerName = "BRONKO",
                    TeamId = 1
                },
                new Player
                {
                    Id = 2,
                    Age = 18,
                    Aim = 5,
                    Experience = 5,
                    PlayerName = "r@di0",
                    TeamId = 1
                },
                new Player
                {
                    Id = 3,
                    Age = 25,
                    Aim = 8,
                    Experience = 8,
                    PlayerName = "CNN",
                    TeamId = 1
                },
                new Player
                {
                    Id = 4,
                    Age = 37,
                    Aim = 3,
                    Experience = 8,
                    PlayerName = "rose-bowl",
                    TeamId = 1
                },
                new Player
                {
                    Id = 5,
                    Age = 28,
                    Aim = 9,
                    Experience = 1,
                    PlayerName = "syt",
                    TeamId = 1
                },
                new Player
                {
                    Id = 6,
                    Age = 22,
                    Aim = 10,
                    Experience = 7,
                    PlayerName = "zit9ne",
                    TeamId = 1
                });
            #endregion
        }
    }
}
