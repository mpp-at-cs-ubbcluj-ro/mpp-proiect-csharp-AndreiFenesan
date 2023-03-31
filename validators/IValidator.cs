namespace Teledon.validators;

public interface IValidator<T>
{
    public void Validate(T entity);
}