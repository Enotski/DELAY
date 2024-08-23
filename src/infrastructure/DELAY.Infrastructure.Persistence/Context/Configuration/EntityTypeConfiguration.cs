using DELAY.Core.Domain.Interfaces;
using DELAY.Infrastructure.Persistence.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DELAY.Infrastructure.Persistence.Context.Configuration
{
    internal class EntityTypeConfiguration
    {
        internal static class DbConfigureInterface
        {
            public static void Description<TEntity>(EntityTypeBuilder<TEntity> builder, int maxLength = 2048) where TEntity : class, IDescriptioin
            {
                builder.Property(p => p.Description).HasMaxLength(maxLength);
            }

            public static void Name<TEntity>(EntityTypeBuilder<TEntity> builder) where TEntity : class, IName
            {
                builder.Property(p => p.Name).HasMaxLength(128).IsRequired();
            }
        }
        internal abstract class DbKeyModelEntityConfiguration<TEntity>
            : IEntityTypeConfiguration<TEntity>
            where TEntity : class, IKey
        {
            protected string TableName;
            protected string PrimaryKeyPropertyName;

            protected void IntiTableAndPrimaryKeyNames(string modelName)
            {
                TableName = modelName + "s";
                PrimaryKeyPropertyName = modelName + "Id";
            }

            public virtual void Configure(EntityTypeBuilder<TEntity> builder)
            {
                IntiTableAndPrimaryKeyNames(typeof(TEntity).Name.Replace("Entity", ""));

                builder.ToTable(TableName).HasKey(p => p.Id);

                builder.Property(p => p.Id).HasColumnName(PrimaryKeyPropertyName);
            }
        }

        internal sealed class DbTicketEntityConfiguration
            : DbKeyModelEntityConfiguration<TicketEntity>
        {
            public override void Configure(EntityTypeBuilder<TicketEntity> builder)
            {
                base.Configure(builder);

                builder.HasOne(p => p.ChangedBy).WithMany(p => p.ChangedTickets)
                    .HasForeignKey(p => p.ChangedById).OnDelete(DeleteBehavior.SetNull);

                builder.HasOne(p => p.TicketList).WithMany(p => p.Tickets)
                    .HasForeignKey(p => p.TicketListId).OnDelete(DeleteBehavior.SetNull);

                builder.HasMany(p => p.AssignedUsers).WithOne(p => p.Ticket)
                    .HasForeignKey(p => p.TicketId).OnDelete(DeleteBehavior.Cascade);
            }
        }

        internal sealed class DbTicketsListEntityConfiguration
            : DbKeyModelEntityConfiguration<TicketsListEntity>
        {
            public override void Configure(EntityTypeBuilder<TicketsListEntity> builder)
            {
                base.Configure(builder);

                builder.HasMany(p => p.Tickets).WithOne(p => p.TicketList)
                    .HasForeignKey(p => p.TicketListId).OnDelete(DeleteBehavior.Cascade);
            }
        }

        internal sealed class DbUserEntityConfiguration
            : DbKeyModelEntityConfiguration<UserEntity>
        {
            public override void Configure(EntityTypeBuilder<UserEntity> builder)
            {
                base.Configure(builder);

                builder.HasMany(p => p.AssignedTickets).WithOne(p => p.User)
                    .HasForeignKey(p => p.UserId).OnDelete(DeleteBehavior.Cascade);

                builder.HasMany(p => p.BoardUsers).WithOne(p => p.User)
                    .HasForeignKey(p => p.UserId).OnDelete(DeleteBehavior.Cascade);

                builder.HasMany(p => p.RoomUsers).WithOne(p => p.User)
                    .HasForeignKey(p => p.UserId).OnDelete(DeleteBehavior.Cascade);

                builder.HasMany(p => p.Sessions).WithOne(p => p.User)
                    .HasForeignKey(p => p.UserId).OnDelete(DeleteBehavior.Cascade);
            }
        }

        internal sealed class DbBoardEntityConfiguration
            : DbKeyModelEntityConfiguration<BoardEntity>
        {
            public override void Configure(EntityTypeBuilder<BoardEntity> builder)
            {
                base.Configure(builder);

                DbConfigureInterface.Description(builder);

                builder.HasMany(p => p.BoardUsers).WithOne(p => p.Board)
                    .HasForeignKey(p => p.BoardId).OnDelete(DeleteBehavior.Cascade);

                builder.HasOne(p => p.Room).WithMany(p => p.Boards)
                    .HasForeignKey(p => p.RoomId).OnDelete(DeleteBehavior.SetNull);
            }
        }

        internal sealed class DbRoomEntityConfiguration
            : DbKeyModelEntityConfiguration<RoomEntity>
        {
            public override void Configure(EntityTypeBuilder<RoomEntity> builder)
            {
                base.Configure(builder);

                DbConfigureInterface.Description(builder);

                builder.HasMany(p => p.RoomUsers).WithOne(p => p.Room)
                    .HasForeignKey(p => p.RoomId).OnDelete(DeleteBehavior.Cascade);


                builder.HasMany(p => p.Boards).WithOne(p => p.Room)
                    .HasForeignKey(p => p.RoomId).OnDelete(DeleteBehavior.SetNull);
            }
        }

        internal sealed class DbTicketUserEntityConfiguration
            : DbKeyModelEntityConfiguration<TicketUserEntity>
        {
            public override void Configure(EntityTypeBuilder<TicketUserEntity> builder)
            {
                base.Configure(builder);
            }
        }

        internal sealed class DbTicketUserSessionEntityConfiguration
            : DbKeyModelEntityConfiguration<SessionLogEntity>
        {
            public override void Configure(EntityTypeBuilder<SessionLogEntity> builder)
            {
                base.Configure(builder);

                builder.HasOne(p => p.User).WithMany(p => p.Sessions)
                    .HasForeignKey(p => p.UserId).OnDelete(DeleteBehavior.NoAction);
            }
        }

        internal sealed class DbRoomUserEntityConfiguration
            : DbKeyModelEntityConfiguration<RoomUserEntity>
        {
            public override void Configure(EntityTypeBuilder<RoomUserEntity> builder)
            {
                base.Configure(builder);
            }
        }

        internal sealed class DbBoardUserEntityConfiguration
            : DbKeyModelEntityConfiguration<BoardUserEntity>
        {
            public override void Configure(EntityTypeBuilder<BoardUserEntity> builder)
            {
                base.Configure(builder);
            }
        }
    }
}
