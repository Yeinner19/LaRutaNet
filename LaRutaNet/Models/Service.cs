using System;
using System.Collections.Generic;

namespace LaRutaNet.Models;

public partial class Service
{
    public long Id { get; set; }

    public ulong Active { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? DeletedAt { get; set; }

    public string Description { get; set; } = null!;

    public string Name { get; set; } = null!;

    public string Type { get; set; } = null!;

    public long CommunityId { get; set; }

    public long? UserHistoryId { get; set; }

    public virtual Community Community { get; set; } = null!;

    public virtual ICollection<Post> Posts { get; set; } = new List<Post>();

    public virtual UserFitnessHistory? UserHistory { get; set; }
}
