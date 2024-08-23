using DELAY.Infrastructure.Persistence.Entities;
using Microsoft.EntityFrameworkCore;

namespace DELAY.Infrastructure.Persistence.Context.Configuration
{
    internal static class SeedingConfiguration
    {
        public static void Seed(this ModelBuilder builder)
        {
            builder.Entity<TicketEntity>().HasData(GetPreconfiguredTicketEntities());
            builder.Entity<UserEntity>().HasData(GetPreconfiguredUsetEntities());
            builder.Entity<BoardEntity>().HasData(GetPreconfiguredBoardEntities());
            builder.Entity<RoomEntity>().HasData(GetPreconfiguredRoomEntities());
        }

        private static UserEntity[] GetPreconfiguredUsetEntities()
        {
            return [
                new UserEntity() {
                    Id = Guid.NewGuid(),
                    Name = "Default user 1",
                },
                new UserEntity() {
                    Id = Guid.NewGuid(),
                    Name = "Default user 2",
                },
            ];
        }

        private static TicketEntity[] GetPreconfiguredTicketEntities()
        {
            return [
                new TicketEntity() {
                    Id = Guid.NewGuid(),
                    Name = "Default ticket 1",
                    Description = "Default desctiption",
                    ChangedDate = DateTime.UtcNow,
                },
                new TicketEntity() {
                    Id = Guid.NewGuid(),
                    Name = "Default ticket 2",
                    Description = "Default desctiption",
                    ChangedDate = DateTime.UtcNow,
                },
            ];
        }

        private static BoardEntity[] GetPreconfiguredBoardEntities()
        {
            return [
                new BoardEntity() {
                    Id = Guid.NewGuid(),
                    Name = "Default board 1",
                    Description = "Default desctiption",
                },
                new BoardEntity() {
                    Id = Guid.NewGuid(),
                    Name = "Default board 2",
                    Description = "Default desctiption",
                },
            ];
        }

        private static RoomEntity[] GetPreconfiguredRoomEntities()
        {
            return [
                new RoomEntity() {
                    Id = Guid.NewGuid(),
                    Name = "Default room 1",
                    Description = "Default desctiption",
                },
                new RoomEntity() {
                    Id = Guid.NewGuid(),
                    Name = "Default room 2",
                    Description = "Default desctiption",
                },
            ];
        }
    }
}
