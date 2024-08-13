using DELAY.Core.Domain.Interfaces;
using DELAY.Core.Domain.Models;
using DELAY.Infrastructure.Persistence.Entities.Base;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using static DELAY.Infrastructure.Persistence.Context.Configuration.EntityTypeConfiguration;

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

        internal abstract class DbKeyModelEntityConfiguration<TEntity, TDomain> : DbKeyModelEntityConfiguration<TEntity>
            where TEntity : class, IKey
            where TDomain : class, IKey
        {
            public virtual void Configure(EntityTypeBuilder<TEntity> builder)
            {
                IntiTableAndPrimaryKeyNames(typeof(TDomain).Name);

                builder.ToTable(TableName).HasKey(p => p.Id);

                builder.Property(p => p.Id).HasColumnName(PrimaryKeyPropertyName);
            }
        }

        internal abstract class DbKeyNamedModelEntityConfiguration<TEntity, TDomain>
            : DbKeyModelEntityConfiguration<TEntity, TDomain> 
            where TEntity : class, IKey, IName
            where TDomain : class, IKey, IName
        {
            public override void Configure(EntityTypeBuilder<TEntity> builder)
            {
                base.Configure(builder);

                DbConfigureInterface.Name(builder);
            }
        }

        internal sealed class DbTicketEntityConfiguration
            : DbKeyNamedModelEntityConfiguration<TicketEntity, Ticket>
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
            : DbKeyNamedModelEntityConfiguration<TicketsListEntity, TicketsList>
        {
            public override void Configure(EntityTypeBuilder<TicketsListEntity> builder)
            {
                base.Configure(builder);

                builder.HasMany(p => p.Tickets).WithOne(p => p.TicketList)
                    .HasForeignKey(p => p.TicketListId).OnDelete(DeleteBehavior.Cascade);
            }
        }

        internal sealed class DbUserEntityConfiguration
            : DbKeyNamedModelEntityConfiguration<UserEntity, User>
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
            }
        }

        internal sealed class DbBoardEntityConfiguration
            : DbKeyNamedModelEntityConfiguration<BoardEntity, Board>
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
            : DbKeyNamedModelEntityConfiguration<RoomEntity, Room>
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
                IntiTableAndPrimaryKeyNames("TecketUser");

                base.Configure(builder);
            }
        }

        internal sealed class DbRoomUserEntityConfiguration
            : DbKeyModelEntityConfiguration<RoomUserEntity, RoomUser>
        {
            public override void Configure(EntityTypeBuilder<RoomUserEntity> builder)
            {
                base.Configure(builder);


            }
        }

        internal sealed class DbBoardUserEntityConfiguration
            : DbKeyModelEntityConfiguration<BoardUserEntity, BoardUser>
        {
            public override void Configure(EntityTypeBuilder<BoardUserEntity> builder)
            {
                base.Configure(builder);
            }
        }
    }
}
