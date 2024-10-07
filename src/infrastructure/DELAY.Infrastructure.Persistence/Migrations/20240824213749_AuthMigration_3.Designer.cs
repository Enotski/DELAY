﻿// <auto-generated />
using System;
using DELAY.Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace DELAY.Infrastructure.Persistence.Migrations
{
    [DbContext(typeof(DelayContext))]
    [Migration("20240824213749_AuthMigration_3")]
    partial class AuthMigration_3
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("DELAY.Infrastructure.Persistence.Entities.BoardEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("BoardId");

                    b.Property<DateTime>("ChangeDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("ChangedBy")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime>("CreateDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("CreatedBy")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(2048)
                        .HasColumnType("character varying(2048)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<Guid?>("ChatRoomId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("ChatRoomId");

                    b.ToTable("Boards", (string)null);
                });

            modelBuilder.Entity("DELAY.Infrastructure.Persistence.Entities.BoardUserEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("BoardUserId");

                    b.Property<Guid>("BoardId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid");

                    b.Property<int>("UserRole")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("BoardId");

                    b.HasIndex("UserId");

                    b.ToTable("BoardUsers", (string)null);
                });

            modelBuilder.Entity("DELAY.Infrastructure.Persistence.Entities.ChatRoomEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("ChatRoomId");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(2048)
                        .HasColumnType("character varying(2048)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Rooms", (string)null);
                });

            modelBuilder.Entity("DELAY.Infrastructure.Persistence.Entities.ChatRoomUserEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("RoomUserId");

                    b.Property<Guid>("ChatRoomId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid");

                    b.Property<int>("UserRole")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("ChatRoomId");

                    b.HasIndex("UserId");

                    b.ToTable("ChatRoomUsers", (string)null);
                });

            modelBuilder.Entity("DELAY.Infrastructure.Persistence.Entities.SessionLogEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("SessionLogId");

                    b.Property<int>("AuthProvider")
                        .HasColumnType("integer");

                    b.Property<DateTime>("StartTime")
                        .HasColumnType("timestamp with time zone");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("SessionLogs", (string)null);
                });

            modelBuilder.Entity("DELAY.Infrastructure.Persistence.Entities.TicketEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("TicketId");

                    b.Property<Guid?>("ChangedById")
                        .HasColumnType("uuid");

                    b.Property<DateTime>("ChangedDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<Guid>("TicketListId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("ChangedById");

                    b.HasIndex("TicketListId");

                    b.ToTable("Tickets", (string)null);
                });

            modelBuilder.Entity("DELAY.Infrastructure.Persistence.Entities.TicketUserEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("TicketUserId");

                    b.Property<Guid>("TicketId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("TicketId");

                    b.HasIndex("UserId");

                    b.ToTable("TicketUsers", (string)null);
                });

            modelBuilder.Entity("DELAY.Infrastructure.Persistence.Entities.TicketsListEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("TicketsListId");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("TicketsLists", (string)null);
                });

            modelBuilder.Entity("DELAY.Infrastructure.Persistence.Entities.UserEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("UserId");

                    b.Property<string>("ChangedBy")
                        .HasColumnType("text");

                    b.Property<DateTime>("ChangedDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime>("CreateDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Email")
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.Property<string>("Password")
                        .HasColumnType("text");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("text");

                    b.Property<int>("Role")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.ToTable("Users", (string)null);
                });

            modelBuilder.Entity("DELAY.Infrastructure.Persistence.Entities.BoardEntity", b =>
                {
                    b.HasOne("DELAY.Infrastructure.Persistence.Entities.ChatRoomEntity", "ChatRoom")
                        .WithMany("Boards")
                        .HasForeignKey("ChatRoomId")
                        .OnDelete(DeleteBehavior.SetNull);

                    b.Navigation("ChatRoom");
                });

            modelBuilder.Entity("DELAY.Infrastructure.Persistence.Entities.BoardUserEntity", b =>
                {
                    b.HasOne("DELAY.Infrastructure.Persistence.Entities.BoardEntity", "Board")
                        .WithMany("BoardUsers")
                        .HasForeignKey("BoardId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DELAY.Infrastructure.Persistence.Entities.UserEntity", "User")
                        .WithMany("BoardUsers")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Board");

                    b.Navigation("User");
                });

            modelBuilder.Entity("DELAY.Infrastructure.Persistence.Entities.ChatRoomUserEntity", b =>
                {
                    b.HasOne("DELAY.Infrastructure.Persistence.Entities.ChatRoomEntity", "ChatRoom")
                        .WithMany("ChatRoomUsers")
                        .HasForeignKey("ChatRoomId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DELAY.Infrastructure.Persistence.Entities.UserEntity", "User")
                        .WithMany("ChatRoomUsers")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ChatRoom");

                    b.Navigation("User");
                });

            modelBuilder.Entity("DELAY.Infrastructure.Persistence.Entities.SessionLogEntity", b =>
                {
                    b.HasOne("DELAY.Infrastructure.Persistence.Entities.UserEntity", "User")
                        .WithMany("Sessions")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("DELAY.Infrastructure.Persistence.Entities.TicketEntity", b =>
                {
                    b.HasOne("DELAY.Infrastructure.Persistence.Entities.UserEntity", "ChangedBy")
                        .WithMany("ChangedTickets")
                        .HasForeignKey("ChangedById")
                        .OnDelete(DeleteBehavior.SetNull);

                    b.HasOne("DELAY.Infrastructure.Persistence.Entities.TicketsListEntity", "TicketList")
                        .WithMany("Tickets")
                        .HasForeignKey("TicketListId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ChangedBy");

                    b.Navigation("TicketList");
                });

            modelBuilder.Entity("DELAY.Infrastructure.Persistence.Entities.TicketUserEntity", b =>
                {
                    b.HasOne("DELAY.Infrastructure.Persistence.Entities.TicketEntity", "Ticket")
                        .WithMany("AssignedUsers")
                        .HasForeignKey("TicketId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DELAY.Infrastructure.Persistence.Entities.UserEntity", "User")
                        .WithMany("AssignedTickets")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Ticket");

                    b.Navigation("User");
                });

            modelBuilder.Entity("DELAY.Infrastructure.Persistence.Entities.BoardEntity", b =>
                {
                    b.Navigation("BoardUsers");
                });

            modelBuilder.Entity("DELAY.Infrastructure.Persistence.Entities.ChatRoomEntity", b =>
                {
                    b.Navigation("Boards");

                    b.Navigation("ChatRoomUsers");
                });

            modelBuilder.Entity("DELAY.Infrastructure.Persistence.Entities.TicketEntity", b =>
                {
                    b.Navigation("AssignedUsers");
                });

            modelBuilder.Entity("DELAY.Infrastructure.Persistence.Entities.TicketsListEntity", b =>
                {
                    b.Navigation("Tickets");
                });

            modelBuilder.Entity("DELAY.Infrastructure.Persistence.Entities.UserEntity", b =>
                {
                    b.Navigation("AssignedTickets");

                    b.Navigation("BoardUsers");

                    b.Navigation("ChangedTickets");

                    b.Navigation("ChatRoomUsers");

                    b.Navigation("Sessions");
                });
#pragma warning restore 612, 618
        }
    }
}
