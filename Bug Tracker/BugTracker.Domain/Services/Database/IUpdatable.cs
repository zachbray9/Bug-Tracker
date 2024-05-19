namespace BugTracker.Domain.Services.Database
{
    public interface IUpdatable<T>
    {
        Task<T> UpdateAsync(Guid id, T entity);
    }
}
