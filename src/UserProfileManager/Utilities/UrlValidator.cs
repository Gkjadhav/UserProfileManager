namespace UserProfileManager.Utilities
{
    public static class UrlValidator
    {
        public static bool IsValidLinkedInUrl(String? url)
        {
            if (string.IsNullOrWhiteSpace(url))
            {
                return true; // Allow empty or null values
            }
            if (!Uri.TryCreate(url, UriKind.Absolute, out Uri? uriResult))
            {
                return false; // Invalid scheme
            }
            
            return uriResult.Scheme == Uri.UriSchemeHttps
                && uriResult.Host.Contains("linkedin.com", StringComparison.OrdinalIgnoreCase);
        }
    }
}
