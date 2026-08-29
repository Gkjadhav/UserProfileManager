using UserProfileManager.Models;

namespace UserProfileManager.Services
{
    public interface IUserService
    {
        Task<ServiceResult<User>> AddUserAsync(User user);

        Task<ServiceResult<User>> UpdateUserAsync(User user);

        Task<ServiceResult<bool>> DeleteUserAsync(int userId);

        Task<ServiceResult<PagedResult<User>>> GetUsersAsync(
            int pageNumber, int pageSize, string? searchTerm = null);
    }
}
