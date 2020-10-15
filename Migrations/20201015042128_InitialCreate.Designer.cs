﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using ef_core_5.Data;

namespace ef_core_5.Migrations
{
    [DbContext(typeof(SchoolContext))]
    [Migration("20201015042128_InitialCreate")]
    partial class InitialCreate
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "5.0.0-rc.1.20451.13");

            modelBuilder.Entity("ef_core_5.Data.Assignment", b =>
                {
                    b.Property<int>("AssignmentId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("CourseSectionId")
                        .HasColumnType("INTEGER");

                    b.HasKey("AssignmentId");

                    b.ToTable("Assignments");
                });

            modelBuilder.Entity("ef_core_5.Data.Course", b =>
                {
                    b.Property<int>("CourseId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .HasColumnType("TEXT");

                    b.HasKey("CourseId");

                    b.ToTable("Courses");
                });

            modelBuilder.Entity("ef_core_5.Data.CourseSection", b =>
                {
                    b.Property<int>("CourseSectionId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("CourseId")
                        .HasColumnType("INTEGER");

                    b.Property<int?>("StudentId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("TeacherId")
                        .HasColumnType("INTEGER");

                    b.HasKey("CourseSectionId");

                    b.HasIndex("StudentId");

                    b.HasIndex("TeacherId");

                    b.ToTable("CourseSections");
                });

            modelBuilder.Entity("ef_core_5.Data.CourseSectionEnrollment", b =>
                {
                    b.Property<int>("CourseId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("StudentId")
                        .HasColumnType("INTEGER");

                    b.HasKey("CourseId", "StudentId");

                    b.ToTable("CourseSectionEnrollments");
                });

            modelBuilder.Entity("ef_core_5.Data.Person", b =>
                {
                    b.Property<int>("PersonId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("FirstName")
                        .HasColumnType("INTEGER");

                    b.Property<int>("LastName")
                        .HasColumnType("INTEGER");

                    b.HasKey("PersonId");

                    b.ToTable("Persons");
                });

            modelBuilder.Entity("ef_core_5.Data.Student", b =>
                {
                    b.Property<int>("StudentId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("PersonId")
                        .HasColumnType("INTEGER");

                    b.HasKey("StudentId");

                    b.HasIndex("PersonId");

                    b.ToTable("Students");
                });

            modelBuilder.Entity("ef_core_5.Data.StudentAssignment", b =>
                {
                    b.Property<int>("StudentAssignmentId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.HasKey("StudentAssignmentId");

                    b.ToTable("StudentAssignments");
                });

            modelBuilder.Entity("ef_core_5.Data.Teacher", b =>
                {
                    b.Property<int>("TeacherId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("PersonId")
                        .HasColumnType("INTEGER");

                    b.HasKey("TeacherId");

                    b.HasIndex("PersonId");

                    b.ToTable("Teachers");
                });

            modelBuilder.Entity("ef_core_5.Data.CourseSection", b =>
                {
                    b.HasOne("ef_core_5.Data.Student", null)
                        .WithMany("CourseSections")
                        .HasForeignKey("StudentId");

                    b.HasOne("ef_core_5.Data.Teacher", null)
                        .WithMany("CourseSections")
                        .HasForeignKey("TeacherId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("ef_core_5.Data.Student", b =>
                {
                    b.HasOne("ef_core_5.Data.Person", "Person")
                        .WithMany()
                        .HasForeignKey("PersonId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Person");
                });

            modelBuilder.Entity("ef_core_5.Data.Teacher", b =>
                {
                    b.HasOne("ef_core_5.Data.Person", "Person")
                        .WithMany()
                        .HasForeignKey("PersonId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Person");
                });

            modelBuilder.Entity("ef_core_5.Data.Student", b =>
                {
                    b.Navigation("CourseSections");
                });

            modelBuilder.Entity("ef_core_5.Data.Teacher", b =>
                {
                    b.Navigation("CourseSections");
                });
#pragma warning restore 612, 618
        }
    }
}
