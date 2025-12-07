using System;
using System.Collections.Generic;

namespace LaRutaNet.Models;

public partial class CommunityMembership
{
    public long Id { get; set; }

    public DateTime JoinedAt { get; set; }

    public long CommunityId { get; set; }

    public long UserId { get; set; }

    public virtual Community Community { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
