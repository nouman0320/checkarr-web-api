using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Checkar_webAPI_core.checkarr
{
    public partial class checkarrContext : DbContext
    {
        public virtual DbSet<TokenGen> TokenGen { get; set; }
        public virtual DbSet<UserLog> UserLog { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                //#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseMySql("Server=localhost;User Id=root;Password=Password420;Database=checkarr");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TokenGen>(entity =>
            {
                entity.HasKey(e => e.Idtoken);

                entity.ToTable("token_gen");

                entity.Property(e => e.Idtoken)
                    .HasColumnName("idtoken")
                    .HasColumnType("int(11)");

                entity.Property(e => e.ExpiryTime)
                    .HasColumnName("expiry_time")
                    .HasColumnType("datetime");

                entity.Property(e => e.TokenString)
                    .HasColumnName("token_string")
                    .HasMaxLength(64);

                entity.Property(e => e.TokenType)
                    .IsRequired()
                    .HasColumnName("token_type")
                    .HasMaxLength(45);

                entity.Property(e => e.UserId)
                    .HasColumnName("user_id")
                    .HasColumnType("int(11)");
            });

            modelBuilder.Entity<UserLog>(entity =>
            {
                entity.HasKey(e => e.IduserLog);

                entity.ToTable("user_log");

                entity.HasIndex(e => e.UserEmaill)
                    .HasName("user_emaill_UNIQUE")
                    .IsUnique();

                entity.Property(e => e.IduserLog)
                    .HasColumnName("iduser_log")
                    .HasColumnType("int(11)");

                entity.Property(e => e.UserEmaill)
                    .IsRequired()
                    .HasColumnName("user_emaill")
                    .HasMaxLength(45);

                entity.Property(e => e.UserFullname)
                    .IsRequired()
                    .HasColumnName("user_fullname")
                    .HasMaxLength(45);

                entity.Property(e => e.UserPassword)
                    .IsRequired()
                    .HasColumnName("user_password")
                    .HasMaxLength(45);

                entity.Property(e => e.UserSex)
                    .IsRequired()
                    .HasColumnName("user_sex")
                    .HasMaxLength(45);

                entity.Property(e => e.UserTimestamp)
                    .HasColumnName("user_timestamp")
                    .HasColumnType("timestamp");
            });
        }
    }
}
