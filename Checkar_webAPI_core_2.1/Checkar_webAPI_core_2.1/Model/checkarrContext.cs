using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Checkar_webAPI_core.Model
{
    public partial class checkarrContext : DbContext
    {
        public checkarrContext()
        {
        }

        public checkarrContext(DbContextOptions<checkarrContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Confirmationcode> Confirmationcode { get; set; }
        public virtual DbSet<DisplayPicture> DisplayPicture { get; set; }
        public virtual DbSet<Fan> Fan { get; set; }
        public virtual DbSet<Fashion> Fashion { get; set; }
        public virtual DbSet<Movie> Movie { get; set; }
        public virtual DbSet<Politics> Politics { get; set; }
        public virtual DbSet<Post> Post { get; set; }
        public virtual DbSet<Song> Song { get; set; }
        public virtual DbSet<TokenGen> TokenGen { get; set; }
        public virtual DbSet<UserLog> UserLog { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseMySql("Server=localhost;User Id=root;Password=rokra786;Database=checkarr");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Confirmationcode>(entity =>
            {
                entity.HasKey(e => e.CId);

                entity.ToTable("confirmationcode");

                entity.Property(e => e.CId)
                    .HasColumnName("c_id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.ConfirmationCode1)
                    .IsRequired()
                    .HasColumnName("confirmation_code")
                    .HasColumnType("varchar(45)");

                entity.Property(e => e.ConfirmationType)
                    .HasColumnName("confirmation_type")
                    .HasColumnType("varchar(45)");

                entity.Property(e => e.ExpiryTime).HasColumnType("datetime");

                entity.Property(e => e.GeneratedOn).HasColumnType("datetime");

                entity.Property(e => e.Used)
                    .HasColumnName("used")
                    .HasColumnType("char(1)");

                entity.Property(e => e.UserId)
                    .HasColumnName("user_id")
                    .HasColumnType("int(11)");
            });

            modelBuilder.Entity<DisplayPicture>(entity =>
            {
                entity.ToTable("display_picture");

                entity.HasIndex(e => e.Id)
                    .HasName("id_UNIQUE")
                    .IsUnique();

                entity.HasIndex(e => e.UserId)
                    .HasName("user_id_idx");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Active)
                    .IsRequired()
                    .HasColumnName("active")
                    .HasColumnType("char(1)");

                entity.Property(e => e.CreationDate)
                    .HasColumnName("creation_date")
                    .HasColumnType("datetime");

                entity.Property(e => e.PublicId)
                    .IsRequired()
                    .HasColumnName("public_id")
                    .HasColumnType("varchar(45)");

                entity.Property(e => e.Url)
                    .IsRequired()
                    .HasColumnName("url")
                    .HasColumnType("varchar(500)");

                entity.Property(e => e.UserId)
                    .HasColumnName("user_id")
                    .HasColumnType("int(11)");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.DisplayPicture)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("user_id");
            });

            modelBuilder.Entity<Fan>(entity =>
            {
                entity.HasKey(e => e.FanAutoid);

                entity.ToTable("fan");

                entity.Property(e => e.FanAutoid)
                    .HasColumnName("fan_autoid")
                    .HasColumnType("int(11)");

                entity.Property(e => e.IdFan)
                    .HasColumnName("idFan")
                    .HasColumnType("int(11)");

                entity.Property(e => e.TimeAdded).HasColumnType("datetime");

                entity.Property(e => e.UserId)
                    .HasColumnName("userID")
                    .HasColumnType("int(11)");
            });

            modelBuilder.Entity<Fashion>(entity =>
            {
                entity.HasKey(e => e.IdFashion);

                entity.ToTable("fashion");

                entity.Property(e => e.IdFashion)
                    .HasColumnName("idFashion")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Name).HasColumnType("varchar(45)");

                entity.Property(e => e.Type).HasColumnType("varchar(45)");

                entity.Property(e => e.Year).HasColumnType("varchar(45)");
            });

            modelBuilder.Entity<Movie>(entity =>
            {
                entity.HasKey(e => e.IdMovie);

                entity.ToTable("movie");

                entity.Property(e => e.IdMovie)
                    .HasColumnName("idMovie")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Genre).HasColumnType("varchar(45)");

                entity.Property(e => e.ReleaseDate).HasColumnType("datetime");

                entity.Property(e => e.Ups).HasColumnType("int(11)");
            });

            modelBuilder.Entity<Politics>(entity =>
            {
                entity.HasKey(e => e.PoliticsPostId);

                entity.ToTable("politics");

                entity.HasIndex(e => e.UserId)
                    .HasName("iduser_log_idx");

                entity.Property(e => e.PoliticsPostId)
                    .HasColumnName("politics_post_id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.PictureUrl)
                    .HasColumnName("Picture_url")
                    .HasColumnType("varchar(100)");

                entity.Property(e => e.TextMessage)
                    .HasColumnName("Text_message")
                    .HasColumnType("varchar(1000)");

                entity.Property(e => e.Time).HasColumnType("datetime");

                entity.Property(e => e.UserId)
                    .HasColumnName("User_id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.VideoUrl)
                    .HasColumnName("Video_url")
                    .HasColumnType("varchar(100)");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Politics)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("iduser_log");
            });

            modelBuilder.Entity<Post>(entity =>
            {
                entity.ToTable("post");

                entity.Property(e => e.Postid)
                    .HasColumnName("POSTID")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Date).HasColumnType("datetime");

                entity.Property(e => e.Fktype)
                    .HasColumnName("FKType")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Sugcount).HasColumnType("int(11)");

                entity.Property(e => e.Type).HasColumnType("varchar(45)");

                entity.Property(e => e.UpVote).HasColumnType("int(11)");

                entity.Property(e => e.UserId)
                    .HasColumnName("UserID")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Views).HasColumnType("int(11)");
            });

            modelBuilder.Entity<Song>(entity =>
            {
                entity.HasKey(e => e.IdSong);

                entity.ToTable("song");

                entity.Property(e => e.IdSong)
                    .HasColumnName("idSong")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Genre).HasColumnType("varchar(45)");

                entity.Property(e => e.SongAdded).HasColumnType("datetime");

                entity.Property(e => e.SongName)
                    .IsRequired()
                    .HasColumnType("varchar(45)");
            });

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
                    .HasColumnType("varchar(64)");

                entity.Property(e => e.TokenType)
                    .IsRequired()
                    .HasColumnName("token_type")
                    .HasColumnType("varchar(45)");

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

                entity.Property(e => e.Activated)
                    .HasColumnType("char(1)")
                    .HasDefaultValueSql("'F'");

                entity.Property(e => e.City)
                    .HasColumnName("city")
                    .HasColumnType("varchar(45)");

                entity.Property(e => e.Country)
                    .HasColumnName("country")
                    .HasColumnType("varchar(45)");

                entity.Property(e => e.Disabled)
                    .HasColumnType("char(1)")
                    .HasDefaultValueSql("'F'");

                entity.Property(e => e.PasswordHash)
                    .HasColumnName("password_hash")
                    .HasMaxLength(248);

                entity.Property(e => e.PasswordSalt)
                    .HasColumnName("password_salt")
                    .HasMaxLength(248);

                entity.Property(e => e.UserEmaill)
                    .IsRequired()
                    .HasColumnName("user_emaill")
                    .HasColumnType("varchar(45)");

                entity.Property(e => e.UserFullname)
                    .IsRequired()
                    .HasColumnName("user_fullname")
                    .HasColumnType("varchar(45)");

                entity.Property(e => e.UserReg)
                    .HasColumnName("user_reg")
                    .HasColumnType("datetime");

                entity.Property(e => e.UserSex)
                    .IsRequired()
                    .HasColumnName("user_sex")
                    .HasColumnType("varchar(45)");
            });
        }
    }
}
