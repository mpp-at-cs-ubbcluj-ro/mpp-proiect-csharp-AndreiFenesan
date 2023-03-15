namespace Teledon.models;

public class Entity<TId>
{
    private TId Id { get; set; }

    public Entity(TId id)
    {
        this.Id = id;
    }
}