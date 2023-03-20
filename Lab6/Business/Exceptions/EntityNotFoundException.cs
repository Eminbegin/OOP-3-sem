namespace Business.Exceptions;

public class EntityNotFoundException<T> : Exception
{
    private EntityNotFoundException(string? message) : base(message) { }

    public static EntityNotFoundException<T> WhyCreate(Guid id)
        => new EntityNotFoundException<T>($"{typeof(T).Name} with id {id} was not found.");
}