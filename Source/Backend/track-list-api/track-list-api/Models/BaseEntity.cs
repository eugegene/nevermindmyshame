namespace api.Models;

/// <summary>
///     Базовий клас для всіх основних сутностей.
///     EF автоматично додасть ці поля до кожної таблиці, що його успадковує.
/// </summary>
public abstract class BaseEntity
{
    // EF налаштовується на генерацію Guid v7 для Postgres
    public Guid Id { get; set; }

    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }

    // Для "Soft Delete". Якщо null - запис активний. Якщо дата - видалений.
    public DateTime? DeletedAt { get; set; }
}