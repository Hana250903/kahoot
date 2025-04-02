﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace PRN222.Kahoot.Repository.Models;

public partial class KahootContext : DbContext
{
    public KahootContext(DbContextOptions<KahootContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Participant> Participants { get; set; }

    public virtual DbSet<Question> Questions { get; set; }

    public virtual DbSet<QuestionSession> QuestionSessions { get; set; }

    public virtual DbSet<Quiz> Quizzes { get; set; }

    public virtual DbSet<QuizSession> QuizSessions { get; set; }

    public virtual DbSet<Response> Responses { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Participant>(entity =>
        {
            entity.HasKey(e => e.ParticipantId).HasName("PK__Particip__7227997E504B6824");

            entity.ToTable("Participant");

            entity.Property(e => e.ParticipantId).HasColumnName("ParticipantID");
            entity.Property(e => e.JoinAt).HasColumnType("datetime");
            entity.Property(e => e.Score).HasDefaultValue(0);
            entity.Property(e => e.SessionId).HasColumnName("SessionID");
            entity.Property(e => e.Team)
                .IsRequired()
                .HasMaxLength(50);
            entity.Property(e => e.UserId).HasColumnName("UserID");

            entity.HasOne(d => d.Session).WithMany(p => p.Participants)
                .HasForeignKey(d => d.SessionId)
                .HasConstraintName("FK__Participa__Sessi__6B24EA82");

            entity.HasOne(d => d.User).WithMany(p => p.Participants)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Participa__UserI__6C190EBB");
        });

        modelBuilder.Entity<Question>(entity =>
        {
            entity.HasKey(e => e.QuestionId).HasName("PK__Question__0DC06F8C8640FBC7");

            entity.ToTable("Question");

            entity.Property(e => e.QuestionId).HasColumnName("QuestionID");
            entity.Property(e => e.Question1).IsRequired();
            entity.Property(e => e.Question2).IsRequired();
            entity.Property(e => e.Question3).IsRequired();
            entity.Property(e => e.Question4).IsRequired();
            entity.Property(e => e.QuestionText).IsRequired();
            entity.Property(e => e.QuestionType)
                .IsRequired()
                .HasMaxLength(15)
                .IsUnicode(false);
            entity.Property(e => e.QuizId).HasColumnName("QuizID");

            entity.HasOne(d => d.Quiz).WithMany(p => p.Questions)
                .HasForeignKey(d => d.QuizId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Question_Quiz");
        });

        modelBuilder.Entity<QuestionSession>(entity =>
        {
            entity.ToTable("QuestionSession");

            entity.Property(e => e.QuestionSessionId)
                .ValueGeneratedNever()
                .HasColumnName("QuestionSessionID");
            entity.Property(e => e.EndTime).HasColumnType("datetime");
            entity.Property(e => e.QuestionId).HasColumnName("QuestionID");
            entity.Property(e => e.QuizSessionId).HasColumnName("QuizSessionID");
            entity.Property(e => e.StartTime).HasColumnType("datetime");

            entity.HasOne(d => d.Question).WithMany(p => p.QuestionSessions)
                .HasForeignKey(d => d.QuestionId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_QuestionSession_Question");

            entity.HasOne(d => d.QuizSession).WithMany(p => p.QuestionSessions)
                .HasForeignKey(d => d.QuizSessionId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_QuestionSession_QuizSession");
        });

        modelBuilder.Entity<Quiz>(entity =>
        {
            entity.ToTable("Quiz");

            entity.Property(e => e.QuizId).HasColumnName("QuizID");
            entity.Property(e => e.CreateAt).HasColumnType("datetime");
            entity.Property(e => e.Title)
                .IsRequired()
                .HasMaxLength(250);
            entity.Property(e => e.Visibility)
                .IsRequired()
                .HasMaxLength(20);

            entity.HasOne(d => d.CreateByNavigation).WithMany(p => p.Quizzes)
                .HasForeignKey(d => d.CreateBy)
                .HasConstraintName("FK_Quiz_User");
        });

        modelBuilder.Entity<QuizSession>(entity =>
        {
            entity.HasKey(e => e.SessionId).HasName("PK__QuizSess__C9F49270FA2C8DC8");

            entity.ToTable("QuizSession");

            entity.Property(e => e.SessionId).HasColumnName("SessionID");
            entity.Property(e => e.CodeRoom)
                .IsRequired()
                .HasMaxLength(5);
            entity.Property(e => e.EndTime).HasColumnType("datetime");
            entity.Property(e => e.HostId).HasColumnName("HostID");
            entity.Property(e => e.IsActive).HasDefaultValue(true);
            entity.Property(e => e.QuizId).HasColumnName("QuizID");
            entity.Property(e => e.StartTime)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");

            entity.HasOne(d => d.Host).WithMany(p => p.QuizSessions)
                .HasForeignKey(d => d.HostId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__QuizSessi__HostI__628FA481");

            entity.HasOne(d => d.Quiz).WithMany(p => p.QuizSessions)
                .HasForeignKey(d => d.QuizId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_QuizSession_Quiz");
        });

        modelBuilder.Entity<Response>(entity =>
        {
            entity.HasKey(e => e.ResponseId).HasName("PK__Response__1AAA640C62D1D79E");

            entity.ToTable("Response");

            entity.Property(e => e.ResponseId).HasColumnName("ResponseID");
            entity.Property(e => e.AnsweredAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.ParticipantId).HasColumnName("ParticipantID");
            entity.Property(e => e.QuestionSessionId).HasColumnName("QuestionSessionID");

            entity.HasOne(d => d.Participant).WithMany(p => p.Responses)
                .HasForeignKey(d => d.ParticipantId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Responses__Parti__75A278F5");

            entity.HasOne(d => d.QuestionSession).WithMany(p => p.Responses)
                .HasForeignKey(d => d.QuestionSessionId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Response_QuestionSession1");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PK__Users__1788CCAC5A5C7ADB");

            entity.ToTable("User");

            entity.HasIndex(e => e.Username, "UQ__Users__536C85E466289229").IsUnique();

            entity.Property(e => e.UserId).HasColumnName("UserID");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Password).HasMaxLength(255);
            entity.Property(e => e.Role)
                .IsRequired()
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.Username)
                .IsRequired()
                .HasMaxLength(50);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}