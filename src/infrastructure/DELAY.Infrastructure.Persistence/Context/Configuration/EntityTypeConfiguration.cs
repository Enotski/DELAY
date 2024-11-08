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
        private const string SuffixToReplace = "Entity";

        private static string GetTableName(string modelName)
        {
            return modelName + "s";
        }

        internal abstract class DbKeyModelEntityConfiguration<TEntity> : IEntityTypeConfiguration<TEntity>
            where TEntity : class, IKey
        {
            protected string TableName;
            protected string PrimaryKeyPropertyName;

            protected void IntiTableAndPrimaryKeyNames(string modelName)
            {
                TableName = GetTableName(modelName);
                PrimaryKeyPropertyName = modelName + "Id";
            }

            public virtual void Configure(EntityTypeBuilder<TEntity> builder)
            {
                IntiTableAndPrimaryKeyNames(typeof(TEntity).Name.Replace(SuffixToReplace, ""));

                builder.ToTable(TableName).HasKey(p => p.Id);

                builder.Property(p => p.Id).HasColumnName(PrimaryKeyPropertyName);
            }
        }

        internal sealed class DbTicketEntityConfiguration : DbKeyModelEntityConfiguration<TicketEntity>
        {
            public override void Configure(EntityTypeBuilder<TicketEntity> builder)
            {
                base.Configure(builder);

                builder.HasOne(p => p.TicketList).WithMany(p => p.Tickets)
                    .HasForeignKey(p => p.TicketListId).OnDelete(DeleteBehavior.SetNull);
            }
        }

        internal sealed class DbTicketsListEntityConfiguration : DbKeyModelEntityConfiguration<TicketsListEntity>
        {
            public override void Configure(EntityTypeBuilder<TicketsListEntity> builder)
            {
                base.Configure(builder);
            }
        }

        internal sealed class DbUserEntityConfiguration : DbKeyModelEntityConfiguration<UserEntity>
        {
            public override void Configure(EntityTypeBuilder<UserEntity> builder)
            {
                base.Configure(builder);
            }
        }

        internal sealed class DbBoardEntityConfiguration : DbKeyModelEntityConfiguration<BoardEntity>
        {
            public override void Configure(EntityTypeBuilder<BoardEntity> builder)
            {
                base.Configure(builder);

                builder.HasMany(p => p.TicketsLists).WithOne(p => p.Board)
                    .HasForeignKey(p => p.BoardId).OnDelete(DeleteBehavior.SetNull);
            }
        }

        internal sealed class DbRoomEntityConfiguration : DbKeyModelEntityConfiguration<ChatRoomEntity>
        {
            public override void Configure(EntityTypeBuilder<ChatRoomEntity> builder)
            {
                base.Configure(builder);
            }
        }

        internal sealed class DbSessionLogEntityConfiguration : DbKeyModelEntityConfiguration<SessionLogEntity>
        {
            public override void Configure(EntityTypeBuilder<SessionLogEntity> builder)
            {
                base.Configure(builder);

                builder.HasOne(p => p.User).WithMany(p => p.Sessions)
                    .HasForeignKey(p => p.UserId).OnDelete(DeleteBehavior.NoAction);
            }
        }

        internal sealed class DbBoardUserEntityConfiguration : IEntityTypeConfiguration<BoardUserEntity>
        {
            public void Configure(EntityTypeBuilder<BoardUserEntity> builder)
            {
                builder.ToTable(GetTableName(typeof(BoardUserEntity).Name.Replace(SuffixToReplace, "")))
                                    .HasKey(p => new { p.BoardId, p.UserId });

                builder.HasOne(p => p.User).WithMany(p => p.BoardUsers)
                    .HasForeignKey(p => p.UserId).OnDelete(DeleteBehavior.Cascade);

                builder.HasOne(p => p.Board).WithMany(p => p.BoardUsers)
                    .HasForeignKey(p => p.BoardId).OnDelete(DeleteBehavior.Cascade);
            }
        }

        internal sealed class DbChatRoomUserEntityConfiguration : IEntityTypeConfiguration<ChatRoomUserEntity>
        {
            public void Configure(EntityTypeBuilder<ChatRoomUserEntity> builder)
            {
                builder.ToTable(GetTableName(typeof(ChatRoomUserEntity).Name.Replace(SuffixToReplace, "")))
                    .HasKey(p => new { p.ChatRoomId, p.UserId });

                builder.HasOne(p => p.User).WithMany(p => p.ChatRoomUsers)
                    .HasForeignKey(p => p.UserId).OnDelete(DeleteBehavior.Cascade);

                builder.HasOne(p => p.ChatRoom).WithMany(p => p.ChatRoomUsers)
                    .HasForeignKey(p => p.ChatRoomId).OnDelete(DeleteBehavior.Cascade);
            }
        }

        internal sealed class DbBoardChatRoomEntityConfiguration : IEntityTypeConfiguration<BoardChatRoomEntity>
        {
            public void Configure(EntityTypeBuilder<BoardChatRoomEntity> builder)
            {
                builder.ToTable(GetTableName(typeof(BoardChatRoomEntity).Name.Replace(SuffixToReplace, "")))
                    .HasKey(p => new { p.BoardId, p.ChatRoomId });

                builder.HasOne(p => p.Board).WithMany(p => p.BoardChatRooms)
                    .HasForeignKey(p => p.BoardId).OnDelete(DeleteBehavior.Cascade);

                builder.HasOne(p => p.ChatRoom).WithMany(p => p.BoardChatRooms)
                    .HasForeignKey(p => p.ChatRoomId).OnDelete(DeleteBehavior.Cascade);
            }
        }

        internal sealed class DbChatMessageEntityConfiguration : IEntityTypeConfiguration<ChatMessageEntity>
        {
            public void Configure(EntityTypeBuilder<ChatMessageEntity> builder)
            {
                builder.ToTable(GetTableName(typeof(ChatMessageEntity).Name.Replace(SuffixToReplace, "")))
                    .HasKey(p => new { p.ChatId, p.Author, p.Time });

                builder.HasOne(p => p.Chat).WithMany(p => p.Messages)
                    .HasForeignKey(p => p.ChatId).OnDelete(DeleteBehavior.Cascade);
            }
        }

        internal sealed class DbTicketUserEntityConfiguration : IEntityTypeConfiguration<TicketUserEntity>
        {
            public void Configure(EntityTypeBuilder<TicketUserEntity> builder)
            {
                builder.ToTable(GetTableName(typeof(TicketUserEntity).Name.Replace(SuffixToReplace, "")))
                    .HasKey(p => new { p.TicketId, p.UserId });

                builder.HasOne(p => p.User).WithMany(p => p.AssignedTickets)
                    .HasForeignKey(p => p.UserId).OnDelete(DeleteBehavior.Cascade);

                builder.HasOne(p => p.Ticket).WithMany(p => p.Users)
                    .HasForeignKey(p => p.TicketId).OnDelete(DeleteBehavior.Cascade);
            }
        }
    }
}
