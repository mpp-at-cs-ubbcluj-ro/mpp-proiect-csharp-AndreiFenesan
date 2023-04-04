namespace lab5.validators;

public interface IValidator<T>
{
    public void Validate(T entity);
}