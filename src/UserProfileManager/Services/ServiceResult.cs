namespace UserProfileManager.Services
{
    public class ServiceResult<T>
    {
        public bool Success { get; }
        public T? Value { get; }
        public IReadOnlyList<string>? Errors { get; }

        private ServiceResult(bool success, T? value, IReadOnlyList<string>? errors)
        {
            Success = success;
            Value = value;
            Errors = errors;
        }

        public static ServiceResult<T> Ok(T value)
        {
            return new ServiceResult<T>(true, value, []);
        }

        public static ServiceResult<T> Fail(IReadOnlyList<string> errors)
        {
            return new ServiceResult<T>(false, default, errors);
        }
    }
}
