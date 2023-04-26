namespace model;

public class Entity<TId>
{
    public TId Id { get; set; }

    public Entity(TId id)
    {
        this.Id = id;
    }

    public override string ToString()
    {
        return Id.ToString();
    }
}