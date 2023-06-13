﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Server;

#nullable disable

namespace Server.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "7.0.5");

            modelBuilder.Entity("GameSessionQuestion", b =>
                {
                    b.Property<long>("AskedQuestionsId")
                        .HasColumnType("INTEGER");

                    b.Property<long>("SessionsId")
                        .HasColumnType("INTEGER");

                    b.HasKey("AskedQuestionsId", "SessionsId");

                    b.HasIndex("SessionsId");

                    b.ToTable("GameSessionQuestion");
                });

            modelBuilder.Entity("Server.Models.GameSession", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<long>("StateId")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("StateId");

                    b.ToTable("Sessions");
                });

            modelBuilder.Entity("Server.Models.GameState", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("CorrectAnswers1Player")
                        .HasColumnType("INTEGER");

                    b.Property<int>("CorrectAnswers2Player")
                        .HasColumnType("INTEGER");

                    b.Property<long?>("CurrentQuestionId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("Step")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("CurrentQuestionId");

                    b.ToTable("States");
                });

            modelBuilder.Entity("Server.Models.Question", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Answer1")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Answer2")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Answer3")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Answer4")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("CorrectAnswer")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Text")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Questions");
                });

            modelBuilder.Entity("Server.Models.User", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("ConnectionId")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<long>("GameSessionId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("GameSessionId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("GameSessionQuestion", b =>
                {
                    b.HasOne("Server.Models.Question", null)
                        .WithMany()
                        .HasForeignKey("AskedQuestionsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Server.Models.GameSession", null)
                        .WithMany()
                        .HasForeignKey("SessionsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Server.Models.GameSession", b =>
                {
                    b.HasOne("Server.Models.GameState", "State")
                        .WithMany()
                        .HasForeignKey("StateId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("State");
                });

            modelBuilder.Entity("Server.Models.GameState", b =>
                {
                    b.HasOne("Server.Models.Question", "CurrentQuestion")
                        .WithMany()
                        .HasForeignKey("CurrentQuestionId");

                    b.Navigation("CurrentQuestion");
                });

            modelBuilder.Entity("Server.Models.User", b =>
                {
                    b.HasOne("Server.Models.GameSession", "GameSession")
                        .WithMany("Players")
                        .HasForeignKey("GameSessionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("GameSession");
                });

            modelBuilder.Entity("Server.Models.GameSession", b =>
                {
                    b.Navigation("Players");
                });
#pragma warning restore 612, 618
        }
    }
}
