namespace Core.Service.Extensions
{
    public static class StringExtensions
    {

        /// <summary>
        /// Converts the input string to camelCase
        /// </summary>
        /// <param name="s">The InputString</param>
        /// <returns>The camelCaseString</returns>
        public static string ToCamelCase(this string s) =>
                        s?.Length > 0 ? Char.ToLowerInvariant(s[0]) + s.Substring(1) : s;


    }
}
