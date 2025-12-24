using System.ComponentModel.DataAnnotations;
using api.Enums;

namespace api.Models;

/// <summary>
///     "Ядро" медіа. Зберігає лише не-текстові, незмінні дані.
/// </summary>
public class Media : BaseEntity
{
    // ID з зовнішнього API (напр., "tmdb-12345") для кешування
    [MaxLength(500)] public string? ExternalApiId { get; set; }
    public MediaType Type { get; set; } // Enum

    public int? ReleaseYear { get; set; }
    [MaxLength(500)] public string? PosterUrl { get; set; }

    // Навігаційні властивості
    public virtual ICollection<MediaTranslation> Translations { get; set; } = new List<MediaTranslation>();
    public virtual ICollection<Review> Reviews { get; set; } = new List<Review>();
    public virtual ICollection<TrackingStatus> TrackingEntries { get; set; } = new List<TrackingStatus>();
    public virtual ICollection<PlaylistItem> CollectionItems { get; set; } = new List<PlaylistItem>();
}