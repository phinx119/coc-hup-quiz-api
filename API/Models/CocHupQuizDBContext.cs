using Microsoft.EntityFrameworkCore;

namespace API.Models
{
    public partial class CocHupQuizDBContext : DbContext
    {
        public CocHupQuizDBContext()
        {
        }

        public CocHupQuizDBContext(DbContextOptions<CocHupQuizDBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Category> Categories { get; set; } = null!;
        public virtual DbSet<Difficulty> Difficulties { get; set; } = null!;
        public virtual DbSet<Question> Questions { get; set; } = null!;
        public virtual DbSet<Record> Records { get; set; } = null!;
        public virtual DbSet<User> Users { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                var config = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
                optionsBuilder.UseSqlServer(config.GetConnectionString("MyCnn"));
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>(entity =>
            {
                entity.ToTable("category");

                entity.Property(e => e.CategoryId).HasColumnName("category_id");

                entity.Property(e => e.CategoryName)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("category_name");

                entity.Property(e => e.CategoryValue).HasColumnName("category_value");
            });

            modelBuilder.Entity<Difficulty>(entity =>
            {
                entity.ToTable("difficulty");

                entity.Property(e => e.DifficultyId).HasColumnName("difficulty_id");

                entity.Property(e => e.DifficultyName)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("difficulty_name");
            });

            modelBuilder.Entity<Question>(entity =>
            {
                entity.ToTable("question");

                entity.Property(e => e.QuestionId).HasColumnName("question_id");

                entity.Property(e => e.CategoryId).HasColumnName("category_id");

                entity.Property(e => e.CorrectAnswer)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("correct_answer");

                entity.Property(e => e.DifficultyId).HasColumnName("difficulty_id");

                entity.Property(e => e.IncorrectAnswers)
                    .IsUnicode(false)
                    .HasColumnName("incorrect_answers");

                entity.Property(e => e.QuestionText)
                    .IsUnicode(false)
                    .HasColumnName("question_text");

                entity.Property(e => e.UserId).HasColumnName("user_id");

                entity.HasOne(d => d.Category)
                    .WithMany(p => p.Questions)
                    .HasForeignKey(d => d.CategoryId)
                    .HasConstraintName("FK__question__catego__3F466844");

                entity.HasOne(d => d.Difficulty)
                    .WithMany(p => p.Questions)
                    .HasForeignKey(d => d.DifficultyId)
                    .HasConstraintName("FK__question__diffic__403A8C7D");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Questions)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK__question__user_i__3E52440B");
            });

            modelBuilder.Entity<Record>(entity =>
            {
                entity.ToTable("record");

                entity.Property(e => e.RecordId).HasColumnName("record_id");

                entity.Property(e => e.CategoryId).HasColumnName("category_id");

                entity.Property(e => e.DifficultyId).HasColumnName("difficulty_id");

                entity.Property(e => e.HighScore).HasColumnName("high_score");

                entity.Property(e => e.RecordDate)
                    .HasColumnType("datetime")
                    .HasColumnName("record_date")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.UserId).HasColumnName("user_id");

                entity.HasOne(d => d.Category)
                    .WithMany(p => p.Records)
                    .HasForeignKey(d => d.CategoryId)
                    .HasConstraintName("FK__record__category__44FF419A");

                entity.HasOne(d => d.Difficulty)
                    .WithMany(p => p.Records)
                    .HasForeignKey(d => d.DifficultyId)
                    .HasConstraintName("FK__record__difficul__45F365D3");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Records)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK__record__user_id__440B1D61");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("user");

                entity.HasIndex(e => e.Username, "UQ__user__F3DBC572AEC2B519")
                    .IsUnique();

                entity.Property(e => e.UserId).HasColumnName("user_id");

                entity.Property(e => e.BirthYear).HasColumnName("birth_year");

                entity.Property(e => e.FullName)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("full_name");

                entity.Property(e => e.Password)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("password");

                entity.Property(e => e.Username)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("username");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
