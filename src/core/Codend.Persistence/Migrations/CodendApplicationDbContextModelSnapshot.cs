﻿// <auto-generated />
using System;
using Codend.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Codend.Persistence.Migrations
{
    [DbContext(typeof(CodendApplicationDbContext))]
    partial class CodendApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Codend.Domain.Entities.Project", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("Deleted")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bit")
                        .HasDefaultValue(false);

                    b.Property<Guid>("OwnerId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("OwnerId");

                    b.ToTable("Project");
                });

            modelBuilder.Entity("Codend.Domain.Entities.ProjectTask", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("AssigneeId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("Deleted")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bit")
                        .HasDefaultValue(false);

                    b.Property<string>("Discriminator")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("DueDate")
                        .HasColumnType("datetime2")
                        .HasColumnName("DueDate");

                    b.Property<long?>("EstimatedTime")
                        .HasPrecision(0)
                        .HasColumnType("bigint");

                    b.Property<Guid>("OwnerId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("Priority")
                        .HasColumnType("int")
                        .HasColumnName("ProjectTaskPriority");

                    b.Property<Guid>("ProjectId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("StatusId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<long?>("StoryPoints")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("ProjectId");

                    b.HasIndex("StatusId");

                    b.ToTable("ProjectTask");

                    b.HasDiscriminator<string>("Discriminator").HasValue("ProjectTask");

                    b.UseTphMappingStrategy();
                });

            modelBuilder.Entity("Codend.Domain.Entities.ProjectTaskStatus", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("ProjectId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("ProjectId");

                    b.ToTable("ProjectTaskStatus");
                });

            modelBuilder.Entity("Codend.Domain.Entities.ProjectVersion", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("Deleted")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bit")
                        .HasDefaultValue(false);

                    b.Property<Guid>("ProjectId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("ReleaseDate")
                        .HasPrecision(0)
                        .HasColumnType("datetime2(0)");

                    b.HasKey("Id");

                    b.HasIndex("ProjectId");

                    b.ToTable("ProjectVersion");
                });

            modelBuilder.Entity("Codend.Domain.Entities.Sprint", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("Deleted")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bit")
                        .HasDefaultValue(false);

                    b.Property<DateTime>("DeletedOnUtc")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("ProjectId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("ProjectId");

                    b.ToTable("Sprint");
                });

            modelBuilder.Entity("Codend.Domain.Entities.User", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.ToTable("User");
                });

            modelBuilder.Entity("ProjectMember", b =>
                {
                    b.Property<Guid>("ParticipatingInProjectsId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("ProjectMembersId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("ParticipatingInProjectsId", "ProjectMembersId");

                    b.HasIndex("ProjectMembersId");

                    b.ToTable("ProjectMember");
                });

            modelBuilder.Entity("SprintProjectTask", b =>
                {
                    b.Property<Guid>("AssignedToSprintsId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("SprintProjectTasksId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("AssignedToSprintsId", "SprintProjectTasksId");

                    b.HasIndex("SprintProjectTasksId");

                    b.ToTable("SprintProjectTask");
                });

            modelBuilder.Entity("Codend.Domain.Entities.BugFixProjectTask", b =>
                {
                    b.HasBaseType("Codend.Domain.Entities.ProjectTask");

                    b.HasDiscriminator().HasValue("BugFixProjectTask");
                });

            modelBuilder.Entity("Codend.Domain.Entities.Project", b =>
                {
                    b.HasOne("Codend.Domain.Entities.User", null)
                        .WithMany("ProjectsOwned")
                        .HasForeignKey("OwnerId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.OwnsOne("Codend.Domain.ValueObjects.ProjectDescription", "ProjectDescription", b1 =>
                        {
                            b1.Property<Guid>("ProjectId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<string>("Description")
                                .IsRequired()
                                .HasMaxLength(2000)
                                .HasColumnType("nvarchar(2000)")
                                .HasColumnName("ProjectDescription");

                            b1.HasKey("ProjectId");

                            b1.ToTable("Project");

                            b1.WithOwner()
                                .HasForeignKey("ProjectId");
                        });

                    b.OwnsOne("Codend.Domain.ValueObjects.ProjectName", "ProjectName", b1 =>
                        {
                            b1.Property<Guid>("ProjectId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<string>("Name")
                                .IsRequired()
                                .HasMaxLength(100)
                                .HasColumnType("nvarchar(100)")
                                .HasColumnName("ProjectName");

                            b1.HasKey("ProjectId");

                            b1.ToTable("Project");

                            b1.WithOwner()
                                .HasForeignKey("ProjectId");
                        });

                    b.Navigation("ProjectDescription");

                    b.Navigation("ProjectName")
                        .IsRequired();
                });

            modelBuilder.Entity("Codend.Domain.Entities.ProjectTask", b =>
                {
                    b.HasOne("Codend.Domain.Entities.Project", null)
                        .WithMany("ProjectTasks")
                        .HasForeignKey("ProjectId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("Codend.Domain.Entities.ProjectTaskStatus", null)
                        .WithMany()
                        .HasForeignKey("StatusId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.OwnsOne("Codend.Domain.ValueObjects.ProjectTaskDescription", "Description", b1 =>
                        {
                            b1.Property<Guid>("ProjectTaskId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<string>("Description")
                                .IsRequired()
                                .HasMaxLength(2000)
                                .HasColumnType("nvarchar(2000)")
                                .HasColumnName("Description");

                            b1.HasKey("ProjectTaskId");

                            b1.ToTable("ProjectTask");

                            b1.WithOwner()
                                .HasForeignKey("ProjectTaskId");
                        });

                    b.OwnsOne("Codend.Domain.ValueObjects.ProjectTaskName", "Name", b1 =>
                        {
                            b1.Property<Guid>("ProjectTaskId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<string>("Name")
                                .IsRequired()
                                .HasMaxLength(150)
                                .HasColumnType("nvarchar(150)")
                                .HasColumnName("Name");

                            b1.HasKey("ProjectTaskId");

                            b1.ToTable("ProjectTask");

                            b1.WithOwner()
                                .HasForeignKey("ProjectTaskId");
                        });

                    b.Navigation("Description");

                    b.Navigation("Name")
                        .IsRequired();
                });

            modelBuilder.Entity("Codend.Domain.Entities.ProjectTaskStatus", b =>
                {
                    b.HasOne("Codend.Domain.Entities.Project", null)
                        .WithMany()
                        .HasForeignKey("ProjectId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.OwnsOne("Codend.Domain.ValueObjects.ProjectTaskStatusName", "Name", b1 =>
                        {
                            b1.Property<Guid>("ProjectTaskStatusId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<string>("Name")
                                .IsRequired()
                                .HasMaxLength(150)
                                .HasColumnType("nvarchar(150)")
                                .HasColumnName("Name");

                            b1.HasKey("ProjectTaskStatusId");

                            b1.ToTable("ProjectTaskStatus");

                            b1.WithOwner()
                                .HasForeignKey("ProjectTaskStatusId");
                        });

                    b.Navigation("Name")
                        .IsRequired();
                });

            modelBuilder.Entity("Codend.Domain.Entities.ProjectVersion", b =>
                {
                    b.HasOne("Codend.Domain.Entities.Project", null)
                        .WithMany("ProjectVersions")
                        .HasForeignKey("ProjectId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.OwnsOne("Codend.Domain.ValueObjects.ProjectVersionChangelog", "Changelog", b1 =>
                        {
                            b1.Property<Guid>("ProjectVersionId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<string>("Changelog")
                                .IsRequired()
                                .HasMaxLength(3000)
                                .HasColumnType("nvarchar(3000)")
                                .HasColumnName("Changelog");

                            b1.HasKey("ProjectVersionId");

                            b1.ToTable("ProjectVersion");

                            b1.WithOwner()
                                .HasForeignKey("ProjectVersionId");
                        });

                    b.OwnsOne("Codend.Domain.ValueObjects.ProjectVersionName", "VersionName", b1 =>
                        {
                            b1.Property<Guid>("ProjectVersionId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<string>("Name")
                                .IsRequired()
                                .HasMaxLength(50)
                                .HasColumnType("nvarchar(50)")
                                .HasColumnName("VersionName");

                            b1.HasKey("ProjectVersionId");

                            b1.ToTable("ProjectVersion");

                            b1.WithOwner()
                                .HasForeignKey("ProjectVersionId");
                        });

                    b.OwnsOne("Codend.Domain.ValueObjects.ProjectVersionTag", "VersionTag", b1 =>
                        {
                            b1.Property<Guid>("ProjectVersionId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<string>("Tag")
                                .IsRequired()
                                .HasMaxLength(20)
                                .HasColumnType("nvarchar(20)")
                                .HasColumnName("VersionTag");

                            b1.HasKey("ProjectVersionId");

                            b1.ToTable("ProjectVersion");

                            b1.WithOwner()
                                .HasForeignKey("ProjectVersionId");
                        });

                    b.Navigation("Changelog");

                    b.Navigation("VersionName");

                    b.Navigation("VersionTag")
                        .IsRequired();
                });

            modelBuilder.Entity("Codend.Domain.Entities.Sprint", b =>
                {
                    b.HasOne("Codend.Domain.Entities.Project", null)
                        .WithMany("Sprints")
                        .HasForeignKey("ProjectId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.OwnsOne("Codend.Domain.ValueObjects.SprintGoal", "Goal", b1 =>
                        {
                            b1.Property<Guid>("SprintId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<string>("Goal")
                                .IsRequired()
                                .HasMaxLength(200)
                                .HasColumnType("nvarchar(200)")
                                .HasColumnName("Goal");

                            b1.HasKey("SprintId");

                            b1.ToTable("Sprint");

                            b1.WithOwner()
                                .HasForeignKey("SprintId");
                        });

                    b.OwnsOne("Codend.Domain.ValueObjects.SprintPeriod", "Period", b1 =>
                        {
                            b1.Property<Guid>("SprintId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<DateTime>("EndDate")
                                .HasPrecision(0)
                                .HasColumnType("datetime2(0)")
                                .HasColumnName("EndDate");

                            b1.Property<DateTime>("StartDate")
                                .HasPrecision(0)
                                .HasColumnType("datetime2(0)")
                                .HasColumnName("StartDate");

                            b1.HasKey("SprintId");

                            b1.ToTable("Sprint");

                            b1.WithOwner()
                                .HasForeignKey("SprintId");
                        });

                    b.Navigation("Goal");

                    b.Navigation("Period")
                        .IsRequired();
                });

            modelBuilder.Entity("ProjectMember", b =>
                {
                    b.HasOne("Codend.Domain.Entities.Project", null)
                        .WithMany()
                        .HasForeignKey("ParticipatingInProjectsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Codend.Domain.Entities.User", null)
                        .WithMany()
                        .HasForeignKey("ProjectMembersId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("SprintProjectTask", b =>
                {
                    b.HasOne("Codend.Domain.Entities.Sprint", null)
                        .WithMany()
                        .HasForeignKey("AssignedToSprintsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Codend.Domain.Entities.ProjectTask", null)
                        .WithMany()
                        .HasForeignKey("SprintProjectTasksId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Codend.Domain.Entities.Project", b =>
                {
                    b.Navigation("ProjectTasks");

                    b.Navigation("ProjectVersions");

                    b.Navigation("Sprints");
                });

            modelBuilder.Entity("Codend.Domain.Entities.User", b =>
                {
                    b.Navigation("ProjectsOwned");
                });
#pragma warning restore 612, 618
        }
    }
}
