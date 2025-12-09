using System;
using System.Collections.Generic;

namespace LaRutaNet.Models;

public partial class Post
{
    public long Id { get; set; }

    public int? Comentarios { get; set; }

    public int? Compartidos { get; set; }

    public string Contenido { get; set; } = null!;

    public DateTime? FechaCreacion { get; set; }

    public ulong HasMedia { get; set; }

    public string? ImagePublicId { get; set; }

    public string? ImageThumbnailUrl { get; set; }

    public string? ImageUrl { get; set; }

    public int? Likes { get; set; }

    public string Type { get; set; } = null!;

    public DateTime? FechaActualizacion { get; set; }

    public long AuthorId { get; set; }

    public long? CommunityId { get; set; }

    public long? ServiceId { get; set; }

    public long? UsuarioDestinoId { get; set; }

    public virtual User? Author { get; set; } = null!;


    public virtual Community? Community { get; set; }


    public virtual Service? Service { get; set; }

    public virtual User? UsuarioDestino { get; set; }
}