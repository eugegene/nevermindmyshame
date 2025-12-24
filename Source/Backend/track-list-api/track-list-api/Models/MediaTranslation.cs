using System.ComponentModel.DataAnnotations;
using api.Enums;

namespace api.Models;

/// <summary>
///     Таблиця перекладів ("ключ:значення"). Вся текстова інфо тут.
/// </summary>
public class MediaTranslation : BaseEntity
{
    public Guid MediaId { get; set; }
    public virtual Media? Media { get; set; }

    //FIXME зробити чи то повноцінний перелік ленгудж кодів чи то шо. Але я не думаю що це має бути рандомний рядок
    [MaxLength(5)] public string? LanguageCode { get; set; } // "uk", "en"
    [MaxLength(200)] public string? Title { get; set; }
    [MaxLength(10000)] public string? Description { get; set; }

    // Наш ENUM для модерації перекладів
    public TranslationStatus Status { get; set; }

    // Аудит: Хто схвалив/відхилив
    public Guid? ProcessedByUserId { get; set; }
    public virtual User? ProcessedByUser { get; set; }
}