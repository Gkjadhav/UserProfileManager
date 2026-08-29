using UserProfileManager.Models;

namespace UserProfileManager.Repositories
{
    public interface IUserRepository
    {
        Task<User> AddAsync(User user);
        Task<bool> UpdateAsync(User user);
        Task<bool> DeleteAsync(int id);
        Task<IReadOnlyList<User>> GetPageAsync(int pageNumber, int pageSize,
            string? searchTerm = null);
        Task<int> GetTotalCountAsync(string? searchTerm = null);
        Task<bool> UsernameExistsAsync(string username,
            int? excludeUserId = null);
    }
}
