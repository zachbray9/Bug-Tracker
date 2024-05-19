namespace BugTracker.Domain.Services.Database
{
    public interface IWritable<T>
    {
        Task<T> CreateAsync(T entity);
    }
}
