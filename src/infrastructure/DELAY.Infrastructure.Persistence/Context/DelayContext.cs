using DELAY.Infrastructure.Persistence.Entities;
using Microsoft.EntityFrameworkCore;

namespace DELAY.Infrastructure.Persistence.Context
{
    public class DelayContext : DbContext
    {
        public DbSet<TicketEntity> Tickets { get; set; }

        public DbSet<TicketUserEntity> TicketUsers { get; set; }

        public DbSet<TicketsListEntity> TicketsLists { get; set; }

        public DbSet<BoardEntity> Boards { get; set; }

        public DbSet<BoardUserEntity> BoardUsers { get; set; }

        public DbSet<RoomEntity> Rooms { get; set; }

        public DbSet<RoomUserEntity> RoomUsers { get; set; }

        public DbSet<UserEntity> Users { get; set; }

        public DbSet<SessionLogEntity> UserSessions { get; set; }

        public DelayContext(DbContextOptions<DelayContext> options)
            : base(ModifyOptions(options))
        {
            //Database.EnsureDeleted();
            Database.EnsureCreated();
        }

        private static DbContextOptions<DelayContext> ModifyOptions(DbContextOptions<DelayContext> options)
        {
            var optionsBuilder = new DbContextOptionsBuilder<DelayContext>(options);

            optionsBuilder.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);

            return optionsBuilder.Options;
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfigurationsFromAssembly(typeof(DelayContext).Assembly);

            //builder.Seed();

            base.OnModelCreating(builder);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }
    }
}
