using System;
using System.Collections.Generic;

namespace LaRutaNet.Models;

public partial class Like
{
    public long Id { get; set; }

    public DateTime CreatedAt { get; set; }

    public long PostId { get; set; }

    public long UserId { get; set; }

    public virtual Post Post { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
