using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Pomelo.EntityFrameworkCore.MySql.Scaffolding.Internal;

namespace LaRutaNet.Models;

public partial class LarutaContext : DbContext
{
    public LarutaContext()
    {
    }

    public LarutaContext(DbContextOptions<LarutaContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Comment> Comments { get; set; }

    public virtual DbSet<Community> Communities { get; set; }

    public virtual DbSet<CommunityMembership> CommunityMemberships { get; set; }

    public virtual DbSet<Like> Likes { get; set; }

    public virtual DbSet<Post> Posts { get; set; }

    public virtual DbSet<Service> Services { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<UserFitnessHistory> UserFitnessHistories { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {

    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .UseCollation("utf8mb4_0900_ai_ci")
            .HasCharSet("utf8mb4");

        modelBuilder.Entity<Comment>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("comments");

            entity.HasIndex(e => e.ParentCommentId, "FK7h839m3lkvhbyv3bcdv7sm4fj");

            entity.HasIndex(e => e.UserId, "FK8omq0tc18jd43bu5tjh6jvraq");

            entity.HasIndex(e => e.PostId, "FKbqnvawwwv4gtlctsi3o7vs131");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Activo)
                .HasColumnType("bit(1)")
                .HasColumnName("activo");
            entity.Property(e => e.Contenido)
                .HasMaxLength(1000)
                .HasColumnName("contenido");
            entity.Property(e => e.FechaActualizacion)
                .HasMaxLength(6)
                .HasColumnName("fecha_actualizacion");
            entity.Property(e => e.FechaCreacion)
                .HasMaxLength(6)
                .HasColumnName("fecha_creacion");
            entity.Property(e => e.LikesCount).HasColumnName("likes_count");
            entity.Property(e => e.ParentCommentId).HasColumnName("parent_comment_id");
            entity.Property(e => e.PostId).HasColumnName("post_id");
            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.HasOne(d => d.ParentComment).WithMany(p => p.InverseParentComment)
                .HasForeignKey(d => d.ParentCommentId)
                .HasConstraintName("FK7h839m3lkvhbyv3bcdv7sm4fj");

            entity.HasOne(d => d.Post).WithMany(p => p.Comments)
                .HasForeignKey(d => d.PostId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FKbqnvawwwv4gtlctsi3o7vs131");

            entity.HasOne(d => d.User).WithMany(p => p.Comments)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK8omq0tc18jd43bu5tjh6jvraq");
        });

        modelBuilder.Entity<Community>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("communities");

            entity.HasIndex(e => e.CreatorId, "FKehds1lhi1y9a8rweslp1esncn");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Active)
                .HasColumnType("bit(1)")
                .HasColumnName("active");
            entity.Property(e => e.AllowsPosts)
                .HasColumnType("bit(1)")
                .HasColumnName("allows_posts");
            entity.Property(e => e.BannerPublicId)
                .HasMaxLength(200)
                .HasColumnName("banner_public_id");
            entity.Property(e => e.BannerUrl)
                .HasMaxLength(500)
                .HasColumnName("banner_url");
            entity.Property(e => e.Category)
                .HasColumnType("enum('FITNESS','NUTRITION','PERSONAL_DEVELOPMENT')")
                .HasColumnName("category");
            entity.Property(e => e.CreatorId).HasColumnName("creator_id");
            entity.Property(e => e.DateOfCreation)
                .HasMaxLength(6)
                .HasColumnName("date_of_creation");
            entity.Property(e => e.DeletedAt)
                .HasMaxLength(6)
                .HasColumnName("deleted_at");
            entity.Property(e => e.Description)
                .HasMaxLength(250)
                .HasColumnName("description");
            entity.Property(e => e.LogoPublicId)
                .HasMaxLength(200)
                .HasColumnName("logo_public_id");
            entity.Property(e => e.LogoUrl)
                .HasMaxLength(500)
                .HasColumnName("logo_url");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .HasColumnName("name");
            entity.Property(e => e.PostRules)
                .HasColumnType("text")
                .HasColumnName("post_rules");

            entity.HasOne(d => d.Creator).WithMany(p => p.Communities)
                .HasForeignKey(d => d.CreatorId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FKehds1lhi1y9a8rweslp1esncn");
        });

        modelBuilder.Entity<CommunityMembership>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("community_memberships");

            entity.HasIndex(e => e.UserId, "FKgju0tl907079t8q0uo730jm82");

            entity.HasIndex(e => new { e.CommunityId, e.UserId }, "UK31crb04sfw3hii1grpih8t5l2").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CommunityId).HasColumnName("community_id");
            entity.Property(e => e.JoinedAt)
                .HasMaxLength(6)
                .HasColumnName("joined_at");
            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.HasOne(d => d.Community).WithMany(p => p.CommunityMemberships)
                .HasForeignKey(d => d.CommunityId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FKhuw0q35omchguvw829no1pdv4");

            entity.HasOne(d => d.User).WithMany(p => p.CommunityMemberships)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FKgju0tl907079t8q0uo730jm82");
        });

        modelBuilder.Entity<Like>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("likes");

            entity.HasIndex(e => e.UserId, "fk_like_user");

            entity.HasIndex(e => new { e.PostId, e.UserId }, "uk_like_post_user").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CreatedAt)
                .HasMaxLength(6)
                .HasColumnName("created_at");
            entity.Property(e => e.PostId).HasColumnName("post_id");
            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.HasOne(d => d.Post).WithMany(p => p.LikesNavigation)
                .HasForeignKey(d => d.PostId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_like_post");

            entity.HasOne(d => d.User).WithMany(p => p.Likes)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_like_user");
        });

        modelBuilder.Entity<Post>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("post");

            entity.HasIndex(e => e.AuthorId, "FK1mpebp1ayl0twrwm7ruiof778");

            entity.HasIndex(e => e.CommunityId, "FKbq8sqiytubaf9nswqm77tpa44");

            entity.HasIndex(e => e.UsuarioDestinoId, "FKixpkm80leprfn0lvnuwhpn8h");

            entity.HasIndex(e => e.ServiceId, "FKonbuu5ko2934qjn0o26mf0yg2");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.AuthorId).HasColumnName("author_id");
            entity.Property(e => e.Comentarios).HasColumnName("comentarios");
            entity.Property(e => e.CommunityId).HasColumnName("community_id");
            entity.Property(e => e.Compartidos).HasColumnName("compartidos");
            entity.Property(e => e.Contenido)
                .HasMaxLength(2000)
                .HasColumnName("contenido");
            entity.Property(e => e.FechaActualizacion)
                .HasMaxLength(6)
                .HasColumnName("fecha_actualizacion");
            entity.Property(e => e.FechaCreacion)
                .HasMaxLength(6)
                .HasColumnName("fecha_creacion");
            entity.Property(e => e.HasMedia)
                .HasColumnType("bit(1)")
                .HasColumnName("has_media");
            entity.Property(e => e.ImagePublicId)
                .HasMaxLength(200)
                .HasColumnName("image_public_id");
            entity.Property(e => e.ImageThumbnailUrl)
                .HasMaxLength(500)
                .HasColumnName("image_thumbnail_url");
            entity.Property(e => e.ImageUrl)
                .HasMaxLength(500)
                .HasColumnName("image_url");
            entity.Property(e => e.Likes).HasColumnName("likes");
            entity.Property(e => e.ServiceId).HasColumnName("service_id");
            entity.Property(e => e.Type)
                .HasColumnType("enum('COMMUNITY','SERVICE','USER')")
                .HasColumnName("type");
            entity.Property(e => e.UsuarioDestinoId).HasColumnName("usuario_destino_id");

            entity.HasOne(d => d.Author).WithMany(p => p.PostAuthors)
                .HasForeignKey(d => d.AuthorId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK1mpebp1ayl0twrwm7ruiof778");

            entity.HasOne(d => d.Community).WithMany(p => p.Posts)
                .HasForeignKey(d => d.CommunityId)
                .HasConstraintName("FKbq8sqiytubaf9nswqm77tpa44");

            entity.HasOne(d => d.Service).WithMany(p => p.Posts)
                .HasForeignKey(d => d.ServiceId)
                .HasConstraintName("FKonbuu5ko2934qjn0o26mf0yg2");

            entity.HasOne(d => d.UsuarioDestino).WithMany(p => p.PostUsuarioDestinos)
                .HasForeignKey(d => d.UsuarioDestinoId)
                .HasConstraintName("FKixpkm80leprfn0lvnuwhpn8h");
        });

        modelBuilder.Entity<Service>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("services");

            entity.HasIndex(e => e.UserHistoryId, "FKmoovntl8xc7oqjvibw1drgh01");

            entity.HasIndex(e => e.CommunityId, "FKp7gq74idqa3xo14t68e7rkluv");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Active)
                .HasColumnType("bit(1)")
                .HasColumnName("active");
            entity.Property(e => e.CommunityId).HasColumnName("community_id");
            entity.Property(e => e.CreatedAt)
                .HasMaxLength(6)
                .HasColumnName("created_at");
            entity.Property(e => e.DeletedAt)
                .HasMaxLength(6)
                .HasColumnName("deleted_at");
            entity.Property(e => e.Description)
                .HasMaxLength(255)
                .HasColumnName("description");
            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .HasColumnName("name");
            entity.Property(e => e.Type)
                .HasColumnType("enum('ACTIVIDAD_FISICA','DEPORTE','NUTRICION')")
                .HasColumnName("type");
            entity.Property(e => e.UserHistoryId).HasColumnName("user_history_id");

            entity.HasOne(d => d.Community).WithMany(p => p.Services)
                .HasForeignKey(d => d.CommunityId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FKp7gq74idqa3xo14t68e7rkluv");

            entity.HasOne(d => d.UserHistory).WithMany(p => p.Services)
                .HasForeignKey(d => d.UserHistoryId)
                .HasConstraintName("FKmoovntl8xc7oqjvibw1drgh01");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("users");

            entity.HasIndex(e => e.Email, "UK6dotkott2kjsp8vw4d0m25fb7").IsUnique();

            entity.HasIndex(e => e.Username, "UKr43af9ap4edm43mmtq01oddj6").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Active)
                .HasColumnType("bit(1)")
                .HasColumnName("active");
            entity.Property(e => e.AvatarPublicId)
                .HasMaxLength(200)
                .HasColumnName("avatar_public_id");
            entity.Property(e => e.AvatarUrl)
                .HasMaxLength(500)
                .HasColumnName("avatar_url");
            entity.Property(e => e.BannerPublicId)
                .HasMaxLength(200)
                .HasColumnName("banner_public_id");
            entity.Property(e => e.BannerUrl)
                .HasMaxLength(500)
                .HasColumnName("banner_url");
            entity.Property(e => e.DateOfBirth).HasColumnName("date_of_birth");
            entity.Property(e => e.DateOfCreation)
                .HasMaxLength(6)
                .HasColumnName("date_of_creation");
            entity.Property(e => e.DeletedAt)
                .HasMaxLength(6)
                .HasColumnName("deleted_at");
            entity.Property(e => e.Email)
                .HasMaxLength(100)
                .HasColumnName("email");
            entity.Property(e => e.EmailVerified)
                .HasColumnType("bit(1)")
                .HasColumnName("email_verified");
            entity.Property(e => e.FirstName)
                .HasMaxLength(50)
                .HasColumnName("first_name");
            entity.Property(e => e.Gender)
                .HasMaxLength(1)
                .HasColumnName("gender");
            entity.Property(e => e.Height).HasColumnName("height");
            entity.Property(e => e.LastName)
                .HasMaxLength(50)
                .HasColumnName("last_name");
            entity.Property(e => e.Password)
                .HasMaxLength(255)
                .HasColumnName("password");
            entity.Property(e => e.PasswordUpdatedAt)
                .HasMaxLength(6)
                .HasColumnName("password_updated_at");
            entity.Property(e => e.ResetPasswordToken)
                .HasMaxLength(100)
                .HasColumnName("reset_password_token");
            entity.Property(e => e.ResetPasswordTokenExpiry)
                .HasMaxLength(6)
                .HasColumnName("reset_password_token_expiry");
            entity.Property(e => e.Role)
                .HasMaxLength(7)
                .HasColumnName("role");
            entity.Property(e => e.SecondLastName)
                .HasMaxLength(50)
                .HasColumnName("second_last_name");
            entity.Property(e => e.SecondName)
                .HasMaxLength(50)
                .HasColumnName("second_name");
            entity.Property(e => e.Username)
                .HasMaxLength(70)
                .HasColumnName("username");
            entity.Property(e => e.VerificationToken)
                .HasMaxLength(100)
                .HasColumnName("verification_token");
            entity.Property(e => e.VerificationTokenExpiry)
                .HasMaxLength(6)
                .HasColumnName("verification_token_expiry");
            entity.Property(e => e.Weight).HasColumnName("weight");
        });

        modelBuilder.Entity<UserFitnessHistory>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("user_fitness_history");

            entity.HasIndex(e => e.UserId, "UKhgux6i5oy4di6n5ekgyh9xts0").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.ArmMeasurement).HasColumnName("arm_measurement");
            entity.Property(e => e.BodyFatPercentage).HasColumnName("body_fat_percentage");
            entity.Property(e => e.ChestMeasurement).HasColumnName("chest_measurement");
            entity.Property(e => e.HipMeasurement).HasColumnName("hip_measurement");
            entity.Property(e => e.RecordDate).HasColumnName("record_date");
            entity.Property(e => e.ThighMeasurement).HasColumnName("thigh_measurement");
            entity.Property(e => e.UserId).HasColumnName("user_id");
            entity.Property(e => e.WaistMeasurement).HasColumnName("waist_measurement");
            entity.Property(e => e.Weight).HasColumnName("weight");

            entity.HasOne(d => d.User).WithOne(p => p.UserFitnessHistory)
                .HasForeignKey<UserFitnessHistory>(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FKgkakt7qamlnyh7bhjeuv4ionm");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
