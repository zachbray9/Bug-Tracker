namespace BugTracker.Domain.Services.Database
{
    public interface IDeletable<T>
    {
        Task<bool> DeleteAsync(Guid id);
    }
}
