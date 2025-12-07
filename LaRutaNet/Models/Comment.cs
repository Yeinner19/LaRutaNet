using System;
using System.Collections.Generic;

namespace LaRutaNet.Models;

public partial class Comment
{
    public long Id { get; set; }

    public ulong Activo { get; set; }

    public string Contenido { get; set; } = null!;

    public DateTime FechaCreacion { get; set; }

    public int? LikesCount { get; set; }

    public DateTime? FechaActualizacion { get; set; }

    public long UserId { get; set; }

    public long? ParentCommentId { get; set; }

    public long PostId { get; set; }

    public virtual ICollection<Comment> InverseParentComment { get; set; } = new List<Comment>();

    public virtual Comment? ParentComment { get; set; }

    public virtual Post Post { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
