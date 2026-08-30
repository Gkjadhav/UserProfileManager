using System;

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

            if (!string.Equals(uriResult.Scheme, Uri.UriSchemeHttps, StringComparison.OrdinalIgnoreCase))
                return false;

            return string.Equals(uriResult.Host, "linkedin.com", StringComparison.OrdinalIgnoreCase)
                || string.Equals(uriResult.Host, "www.linkedin.com", StringComparison.OrdinalIgnoreCase);
        }
    }
}
