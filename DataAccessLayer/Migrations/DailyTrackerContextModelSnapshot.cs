﻿// <auto-generated />
using DataAccessLayer.EntityFramework.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.Storage.Internal;
using System;

namespace DataAccessLayer.EntityFramework.Migrations
{
    [DbContext(typeof(DailyTrackerContext))]
    partial class DailyTrackerContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.0.1-rtm-125");

            modelBuilder.Entity("DataAccessLayer.EntityFramework.Entities.Answer", b =>
                {
                    b.Property<int>("AnswerId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("AdditionalNotes");

                    b.Property<int?>("OptionId");

                    b.Property<int>("ResultId");

                    b.HasKey("AnswerId");

                    b.HasIndex("OptionId");

                    b.HasIndex("ResultId");

                    b.ToTable("Answers");
                });

            modelBuilder.Entity("DataAccessLayer.EntityFramework.Entities.Option", b =>
                {
                    b.Property<int>("OptionId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("OptionText");

                    b.HasKey("OptionId");

                    b.ToTable("Options");
                });

            modelBuilder.Entity("DataAccessLayer.EntityFramework.Entities.Question", b =>
                {
                    b.Property<int>("QuestionId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("QuestionText");

                    b.Property<int?>("QuestionnaireId");

                    b.HasKey("QuestionId");

                    b.HasIndex("QuestionnaireId");

                    b.ToTable("Questions");
                });

            modelBuilder.Entity("DataAccessLayer.EntityFramework.Entities.Questionnaire", b =>
                {
                    b.Property<int>("QuestionnaireId")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreatedDate");

                    b.Property<string>("Description");

                    b.Property<string>("Name");

                    b.HasKey("QuestionnaireId");

                    b.ToTable("Questionnaires");
                });

            modelBuilder.Entity("DataAccessLayer.EntityFramework.Entities.QuestionOption", b =>
                {
                    b.Property<int>("QuestionOptionId")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("OptionId");

                    b.Property<int>("OptionValue");

                    b.Property<int?>("QuestionId");

                    b.HasKey("QuestionOptionId");

                    b.HasIndex("OptionId");

                    b.HasIndex("QuestionId");

                    b.ToTable("QuestionOption");
                });

            modelBuilder.Entity("DataAccessLayer.EntityFramework.Entities.Result", b =>
                {
                    b.Property<int>("ResultId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("AdditionalNotes");

                    b.Property<int>("QuestionnaireId");

                    b.Property<DateTime>("TakenDate");

                    b.Property<int>("UserId");

                    b.HasKey("ResultId");

                    b.HasIndex("QuestionnaireId");

                    b.HasIndex("UserId");

                    b.ToTable("Results");
                });

            modelBuilder.Entity("DataAccessLayer.EntityFramework.Entities.User", b =>
                {
                    b.Property<int>("UserId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("BirthDate");

                    b.Property<string>("FirstName");

                    b.Property<string>("LastName");

                    b.HasKey("UserId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("DataAccessLayer.EntityFramework.Entities.UserDirectory", b =>
                {
                    b.Property<string>("Username")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Password");

                    b.Property<int>("UserId");

                    b.HasKey("Username");

                    b.HasIndex("UserId")
                        .IsUnique();

                    b.ToTable("UserDirectory");
                });

            modelBuilder.Entity("DataAccessLayer.EntityFramework.Entities.Answer", b =>
                {
                    b.HasOne("DataAccessLayer.EntityFramework.Entities.Option")
                        .WithMany("Answers")
                        .HasForeignKey("OptionId");

                    b.HasOne("DataAccessLayer.EntityFramework.Entities.Result", "Result")
                        .WithMany("Answers")
                        .HasForeignKey("ResultId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("DataAccessLayer.EntityFramework.Entities.Question", b =>
                {
                    b.HasOne("DataAccessLayer.EntityFramework.Entities.Questionnaire")
                        .WithMany("Questions")
                        .HasForeignKey("QuestionnaireId");
                });

            modelBuilder.Entity("DataAccessLayer.EntityFramework.Entities.QuestionOption", b =>
                {
                    b.HasOne("DataAccessLayer.EntityFramework.Entities.Option", "Option")
                        .WithMany("QuestionOptions")
                        .HasForeignKey("OptionId");

                    b.HasOne("DataAccessLayer.EntityFramework.Entities.Question", "Question")
                        .WithMany("QuestionOptions")
                        .HasForeignKey("QuestionId");
                });

            modelBuilder.Entity("DataAccessLayer.EntityFramework.Entities.Result", b =>
                {
                    b.HasOne("DataAccessLayer.EntityFramework.Entities.Questionnaire", "Questionnaire")
                        .WithMany("Results")
                        .HasForeignKey("QuestionnaireId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("DataAccessLayer.EntityFramework.Entities.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("DataAccessLayer.EntityFramework.Entities.UserDirectory", b =>
                {
                    b.HasOne("DataAccessLayer.EntityFramework.Entities.User", "User")
                        .WithOne("UserDirectory")
                        .HasForeignKey("DataAccessLayer.EntityFramework.Entities.UserDirectory", "UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
