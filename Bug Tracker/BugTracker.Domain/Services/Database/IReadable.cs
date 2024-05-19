namespace BugTracker.Domain.Services.Database
{
    public interface IReadable<T>
    {
        Task<T> GetByIdAsync(Guid id);
        Task<List<T>> GetAllAsync();
    }
}
