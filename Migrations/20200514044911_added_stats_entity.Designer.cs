﻿// <auto-generated />
using System;
using Fitness_Tracker.Data.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Fitness_Tracker.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20200514044911_added_stats_entity")]
    partial class added_stats_entity
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Fitness_Tracker.Data.Entities.CardioSession", b =>
                {
                    b.Property<int>("CardioSessionId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("CardioSessionDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("Duration")
                        .HasColumnType("int");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("CardioSessionId");

                    b.HasIndex("UserId");

                    b.ToTable("CardioSessions");
                });

            modelBuilder.Entity("Fitness_Tracker.Data.Entities.Excercise", b =>
                {
                    b.Property<int>("ExcerciseId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ExcerciseName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("ResistanceTrainingSessionId")
                        .HasColumnType("int");

                    b.HasKey("ExcerciseId");

                    b.HasIndex("ResistanceTrainingSessionId");

                    b.ToTable("Excercise");
                });

            modelBuilder.Entity("Fitness_Tracker.Data.Entities.ResistanceTrainingSession", b =>
                {
                    b.Property<int>("ResistanceTrainingSessionId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("TrainingSessionDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("ResistanceTrainingSessionId");

                    b.HasIndex("UserId");

                    b.ToTable("ResistanceTrainingSessions");
                });

            modelBuilder.Entity("Fitness_Tracker.Data.Entities.Set", b =>
                {
                    b.Property<int>("SetId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("ExcerciseId")
                        .HasColumnType("int");

                    b.Property<int?>("RateOfPercievedExertion")
                        .HasColumnType("int");

                    b.Property<int>("Reps")
                        .HasColumnType("int");

                    b.Property<decimal>("Weight")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("WeightUnit")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("SetId");

                    b.HasIndex("ExcerciseId");

                    b.ToTable("Set");
                });

            modelBuilder.Entity("Fitness_Tracker.Data.Entities.Stats", b =>
                {
                    b.Property<int>("StatsId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("Age")
                        .HasColumnType("int");

                    b.Property<decimal>("BodyfatPercentage")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("HeightFeet")
                        .HasColumnType("int");

                    b.Property<int>("HeightInch")
                        .HasColumnType("int");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.Property<decimal>("Weight")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("WeightUnit")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("StatsId");

                    b.HasIndex("UserId")
                        .IsUnique();

                    b.ToTable("Stats");
                });

            modelBuilder.Entity("Fitness_Tracker.Data.Entities.User", b =>
                {
                    b.Property<int>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("BirthDate")
                        .HasColumnType("date");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FirstName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Username")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("Fitness_Tracker.Data.Entities.CardioSession", b =>
                {
                    b.HasOne("Fitness_Tracker.Data.Entities.User", "User")
                        .WithMany("CardioSessions")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Fitness_Tracker.Data.Entities.Excercise", b =>
                {
                    b.HasOne("Fitness_Tracker.Data.Entities.ResistanceTrainingSession", "ResistanceTrainingSession")
                        .WithMany("Excercises")
                        .HasForeignKey("ResistanceTrainingSessionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Fitness_Tracker.Data.Entities.ResistanceTrainingSession", b =>
                {
                    b.HasOne("Fitness_Tracker.Data.Entities.User", "User")
                        .WithMany("ResistanceTrainingSessions")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Fitness_Tracker.Data.Entities.Set", b =>
                {
                    b.HasOne("Fitness_Tracker.Data.Entities.Excercise", "Excercise")
                        .WithMany("Sets")
                        .HasForeignKey("ExcerciseId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Fitness_Tracker.Data.Entities.Stats", b =>
                {
                    b.HasOne("Fitness_Tracker.Data.Entities.User", "User")
                        .WithOne("Stats")
                        .HasForeignKey("Fitness_Tracker.Data.Entities.Stats", "UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
