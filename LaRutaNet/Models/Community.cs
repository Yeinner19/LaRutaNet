using LaRutaNet.Data;
using System;
using System.Collections.Generic;

namespace LaRutaNet.Models;

public partial class Community
{
    public long Id { get; set; }

    public ulong Active { get; set; }

    public ulong? AllowsPosts { get; set; }

    public string? BannerPublicId { get; set; }

    public string? BannerUrl { get; set; }

    public string Category { get; set; } = null!;

    public DateTime? DateOfCreation { get; set; }

    public DateTime? DeletedAt { get; set; }

    public string Description { get; set; } = null!;

    public string? LogoPublicId { get; set; }

    public string? LogoUrl { get; set; }

    public string Name { get; set; } = null!;

    public string? PostRules { get; set; }

    public long CreatorId { get; set; }


    public virtual User? Creator { get; set; } = null!;

    public virtual ICollection<Post> Posts { get; set; } = new List<Post>();

    public virtual ICollection<Service> Services { get; set; } = new List<Service>();
}
