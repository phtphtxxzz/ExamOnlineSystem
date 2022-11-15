using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace ExamSystem.Models
{
    public partial class OnlineExamSystemContext : DbContext
    {
        public OnlineExamSystemContext()
        {
        }

        public OnlineExamSystemContext(DbContextOptions<OnlineExamSystemContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Account> Accounts { get; set; } = null!;
        public virtual DbSet<Category> Categories { get; set; } = null!;
        public virtual DbSet<CompletedExam> CompletedExams { get; set; } = null!;
        public virtual DbSet<Exam> Exams { get; set; } = null!;
        public virtual DbSet<ExamAnswer> ExamAnswers { get; set; } = null!;
        public virtual DbSet<Question> Questions { get; set; } = null!;
        public virtual DbSet<User> Users { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("server=localhost;database=OnlineExamSystem;uid=sa;pwd=sa;TrustServerCertificate=true");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Account>(entity =>
            {
                entity.ToTable("ACCOUNT");

                entity.HasIndex(e => e.Email, "UQ__ACCOUNT__161CF724CAB137AC")
                    .IsUnique();

                entity.Property(e => e.AccountId).HasColumnName("ACCOUNT_ID");

                entity.Property(e => e.Email)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("EMAIL");

                entity.Property(e => e.Password)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("PASSWORD");

                entity.Property(e => e.Role)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("ROLE");

                entity.Property(e => e.Username)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("USERNAME");
            });

            modelBuilder.Entity<Category>(entity =>
            {
                entity.ToTable("Category");

                entity.HasIndex(e => e.CategoryName, "UQ__Category__9374460FEABC1CB7")
                    .IsUnique();

                entity.Property(e => e.CategoryId).HasColumnName("CATEGORY_ID");

                entity.Property(e => e.CategoryName)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("CATEGORY_NAME");
            });

            modelBuilder.Entity<CompletedExam>(entity =>
            {
                entity.ToTable("COMPLETED_EXAM");

                entity.Property(e => e.CompletedExamId).HasColumnName("COMPLETED_EXAM_ID");

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasColumnName("CREATED_DATE")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.ExamId).HasColumnName("EXAM_ID");

                entity.Property(e => e.Mark).HasColumnName("MARK");

                entity.Property(e => e.UserId).HasColumnName("USER_ID");

                entity.HasOne(d => d.Exam)
                    .WithMany(p => p.CompletedExams)
                    .HasForeignKey(d => d.ExamId)
                    .HasConstraintName("FK__COMPLETED__EXAM___37A5467C");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.CompletedExams)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK__COMPLETED__USER___36B12243");
            });

            modelBuilder.Entity<Exam>(entity =>
            {
                entity.ToTable("EXAM");

                entity.Property(e => e.ExamId).HasColumnName("EXAM_ID");

                entity.Property(e => e.CategoryId).HasColumnName("CATEGORY_ID");

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasColumnName("CREATED_DATE")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.ExamName)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("EXAM_NAME");

                entity.Property(e => e.Numofquestion).HasColumnName("NUMOFQUESTION");

                entity.Property(e => e.Owner).HasColumnName("OWNER");

                entity.Property(e => e.Pictures)
                    .IsUnicode(false)
                    .HasColumnName("PICTURES");

                entity.Property(e => e.Time).HasColumnName("TIME");

                entity.HasOne(d => d.Category)
                    .WithMany(p => p.Exams)
                    .HasForeignKey(d => d.CategoryId)
                    .HasConstraintName("FK__EXAM__CATEGORY_I__300424B4");

                entity.HasOne(d => d.OwnerNavigation)
                    .WithMany(p => p.Exams)
                    .HasForeignKey(d => d.Owner)
                    .HasConstraintName("FK__EXAM__OWNER__2F10007B");
            });

            modelBuilder.Entity<ExamAnswer>(entity =>
            {
                entity.ToTable("EXAM_ANSWER");

                entity.Property(e => e.ExamAnswerId).HasColumnName("EXAM_ANSWER_ID");

                entity.Property(e => e.Answer).HasColumnName("ANSWER");

                entity.Property(e => e.AnswerContent)
                    .IsUnicode(false)
                    .HasColumnName("ANSWER_CONTENT");

                entity.Property(e => e.CompletedExamId).HasColumnName("COMPLETED_EXAM_ID");

                entity.Property(e => e.CorrectAnswer).HasColumnName("CORRECT_ANSWER");

                entity.Property(e => e.QuestionId).HasColumnName("QUESTION_ID");

                entity.HasOne(d => d.CompletedExam)
                    .WithMany(p => p.ExamAnswers)
                    .HasForeignKey(d => d.CompletedExamId)
                    .HasConstraintName("FK__EXAM_ANSW__COMPL__3A81B327");

                entity.HasOne(d => d.Question)
                    .WithMany(p => p.ExamAnswers)
                    .HasForeignKey(d => d.QuestionId)
                    .HasConstraintName("FK__EXAM_ANSW__QUEST__3B75D760");
            });

            modelBuilder.Entity<Question>(entity =>
            {
                entity.ToTable("QUESTION");

                entity.Property(e => e.QuestionId).HasColumnName("QUESTION_ID");

                entity.Property(e => e.Answer1)
                    .IsUnicode(false)
                    .HasColumnName("ANSWER1");

                entity.Property(e => e.Answer2)
                    .IsUnicode(false)
                    .HasColumnName("ANSWER2");

                entity.Property(e => e.Answer3)
                    .IsUnicode(false)
                    .HasColumnName("ANSWER3");

                entity.Property(e => e.Answer4)
                    .IsUnicode(false)
                    .HasColumnName("ANSWER4");

                entity.Property(e => e.CorrectAnswer).HasColumnName("CORRECT_ANSWER");

                entity.Property(e => e.ExamId).HasColumnName("EXAM_ID");

                entity.Property(e => e.Question1)
                    .IsUnicode(false)
                    .HasColumnName("QUESTION");

                entity.HasOne(d => d.Exam)
                    .WithMany(p => p.Questions)
                    .HasForeignKey(d => d.ExamId)
                    .HasConstraintName("FK__QUESTION__EXAM_I__32E0915F");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("USER");

                entity.HasIndex(e => e.Email, "UQ__USER__161CF7245E8D1212")
                    .IsUnique();

                entity.Property(e => e.UserId)
                    .ValueGeneratedNever()
                    .HasColumnName("USER_ID");

                entity.Property(e => e.Address)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("ADDRESS");

                entity.Property(e => e.Email)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("EMAIL");

                entity.Property(e => e.Gender)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("GENDER");

                entity.Property(e => e.Image)
                    .IsUnicode(false)
                    .HasColumnName("IMAGE");

                entity.Property(e => e.Name)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("NAME");

                entity.Property(e => e.Phone)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("PHONE");

                entity.Property(e => e.Role)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("ROLE");

                entity.HasOne(d => d.UserNavigation)
                    .WithOne(p => p.User)
                    .HasForeignKey<User>(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__USER__USER_ID__286302EC");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
