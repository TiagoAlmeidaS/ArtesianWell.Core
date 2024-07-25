namespace Domain.SeedWork;

public class Entity
{
    public DateTime UpdatedAt { get; set; }
    public DateTime CreatedAt { get; set; }

    public Entity()
    {
        UpdatedAt = DateTime.UtcNow;
        CreatedAt = DateTime.UtcNow;
    }
}