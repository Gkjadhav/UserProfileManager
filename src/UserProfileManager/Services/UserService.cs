using UserProfileManager.Models;
using UserProfileManager.Repositories;
using UserProfileManager.Validators;

namespace UserProfileManager.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<ServiceResult<User>> AddUserAsync(User user)
        {
            var errors = UserValidator.Validate(user).ToList();

            var usernameExists = await _userRepository.UsernameExistsAsync(user.Username);
            if (usernameExists)
                errors.Add($"{user.Username} already exists.");

            if (errors.Count > 0)
                return ServiceResult<User>.Fail(errors.ToArray());

            user.CreatedAt = DateTime.UtcNow;
            try
            {
                await _userRepository.AddAsync(user);
            }
            catch (Microsoft.Data.Sqlite.SqliteException ex)
            when (ex.SqliteErrorCode == 19) // SQLITE_CONSTRAINT
            {
                return ServiceResult<User>.Fail(["A user with this username or email already exists."]);
            }

            return ServiceResult<User>.Ok(user);
        }

        public async Task<ServiceResult<bool>> DeleteUserAsync(int userId)
        {
            var errors = new List<string>();
            var deleted = await _userRepository.DeleteAsync(userId);
            if (!deleted)
                errors.Add("User not found. It may have already been deleted.");

            if (errors.Count > 0)
                return ServiceResult<bool>.Fail(errors.ToArray());

            return ServiceResult<bool>.Ok(true);
        }

        public async Task<ServiceResult<PagedResult<User>>> GetUsersAsync(int pageNumber, int pageSize
            , string? searchTerm = null)
        {
            var items = await _userRepository.GetPageAsync(pageNumber, pageSize, searchTerm);
            var totalCount = await _userRepository.GetTotalCountAsync(searchTerm);

            var result = new PagedResult<User>(items, totalCount, pageNumber, pageSize);

            return ServiceResult<PagedResult<User>>.Ok(result);
        }

        public async Task<ServiceResult<User>> UpdateUserAsync(User user)
        {
            var errors = UserValidator.Validate(user).ToList();

            var usernameExists = await _userRepository.UsernameExistsAsync(user.Username, user.Id);
            if (usernameExists)
                errors.Add($"Username {user.Username} already exists.");

            if (errors.Count > 0)
                return ServiceResult<User>.Fail(errors.ToArray());

            user.UpdatedAt = DateTime.UtcNow;

            try
            {
                var updated = await _userRepository.UpdateAsync(user);
                if (!updated)
                    return ServiceResult<User>.Fail(["This user no longer exists." +
                    " It may have been deleted elsewhere."]);
            }
            catch (Microsoft.Data.Sqlite.SqliteException ex)
            when (ex.SqliteErrorCode == 19) // SQLITE_CONSTRAINT
            {
                return ServiceResult<User>.Fail(["A user with this username or email already exists."]);
            }

            return ServiceResult<User>.Ok(user);
        }
    }
}
