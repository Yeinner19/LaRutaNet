using System;
using System.Collections.Generic;

namespace LaRutaNet.Models;

public partial class User
{
    public long Id { get; set; }

    public ulong Active { get; set; }

    public string? AvatarPublicId { get; set; }

    public string? AvatarUrl { get; set; }

    public string? BannerPublicId { get; set; }

    public string? BannerUrl { get; set; }

    public DateOnly DateOfBirth { get; set; }

    public DateTime? DateOfCreation { get; set; }

    public DateTime? DeletedAt { get; set; }

    public string Email { get; set; } = null!;

    public ulong? EmailVerified { get; set; }

    public string FirstName { get; set; } = null!;

    public string? Gender { get; set; }

    public double? Height { get; set; }

    public string LastName { get; set; } = null!;

    public string Password { get; set; } = null!;

    public DateTime? PasswordUpdatedAt { get; set; }

    public string? ResetPasswordToken { get; set; }

    public DateTime? ResetPasswordTokenExpiry { get; set; }

    public string Role { get; set; } = null!;

    public string? SecondLastName { get; set; }

    public string? SecondName { get; set; }

    public string Username { get; set; } = null!;

    public string? VerificationToken { get; set; }

    public DateTime? VerificationTokenExpiry { get; set; }

    public double? Weight { get; set; }

    public virtual ICollection<Comment> Comments { get; set; } = new List<Comment>();

    public virtual ICollection<Community> Communities { get; set; } = new List<Community>();

    public virtual ICollection<CommunityMembership> CommunityMemberships { get; set; } = new List<CommunityMembership>();

    public virtual ICollection<Like> Likes { get; set; } = new List<Like>();

    public virtual ICollection<Post> PostAuthors { get; set; } = new List<Post>();

    public virtual ICollection<Post> PostUsuarioDestinos { get; set; } = new List<Post>();

    public virtual UserFitnessHistory? UserFitnessHistory { get; set; }
}
