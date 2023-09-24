namespace Web.Validators
{
    public static class UrlValidator
    {
        public static bool IsValidUrl(Uri url)
        {
            if (!url.IsAbsoluteUri)
            {
                return false;
            }

            if (url.Scheme != "http" && url.Scheme != "https")
            {
                return false;
            }

            return true;
        }
    }
}
