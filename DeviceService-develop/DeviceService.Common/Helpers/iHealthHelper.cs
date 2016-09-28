using System;
using System.Security.Cryptography;
using System.Text.RegularExpressions;
using System.Web;

namespace DeviceService.Common.Helpers
{
    /// <summary>
    /// Help methods to generated data for iHealth accounts.
    /// </summary>
    public static class iHealthHelper
    {
        private static readonly Regex RemoveLeadingDigitsRegex = new Regex(@"^[0-9]{1,}", RegexOptions.Compiled);

        private static readonly Regex RemoveNonAlphanumericCharactersRegex = new Regex(@"[^0-9a-z]",
            RegexOptions.Compiled | RegexOptions.IgnoreCase);

        /// <summary>
        /// Used by GeneratePassword.
        /// </summary>
        private static readonly char[] Punctuations = "!@#$%^&*()_-+=[{]};:>|./?".ToCharArray();

        /// <summary>
        /// Characters that start potentially dangerous patterns.
        /// </summary>
        private readonly static char[] StartingChars = { '<', '&' };

        /// <summary>
        /// Generates a 32-character username that will be joined with the ciconnect.com domain name
        /// to create an unique iHealth username. It will also be used as the nickname for the iHealth account.
        /// </summary>
        /// <returns></returns>
        public static string GenerateRandomiHealthUsernamePrefix()
        {
            using (var rng = new RNGCryptoServiceProvider())
            {
                var buf = new byte[64];
                rng.GetBytes(buf);

                // Encode the random bytes into string representation.
                var usernamePrefix = HttpServerUtility.UrlTokenEncode(buf);

                // Ensure that the prefix doesn't start with a number.
                usernamePrefix = RemoveLeadingDigitsRegex.Replace(usernamePrefix, string.Empty);

                // Remove any non-alphanumeric characters.
                usernamePrefix = RemoveNonAlphanumericCharactersRegex.Replace(usernamePrefix, string.Empty);

                // Make it 32 characters at most.
                return usernamePrefix.Length > 32
                    ? usernamePrefix.Substring(0, 32).ToLowerInvariant()
                    : usernamePrefix.ToLowerInvariant();
            }
        }

        /// <summary>
        /// This code was shamelessly lifted from System.Web.Security.Membership.GeneratePassword so that
        /// we could have its functionality, but not have to take a dependency on System.Web. 
        /// See: http://referencesource.microsoft.com/#System.Web/Security/Membership.cs
        /// </summary>
        /// <param name="length">The number of characters in the generated password. The length must be between 1 and 128 characters.</param>
        /// <param name="numberOfNonAlphanumericCharacters">The minimum number of non-alphanumeric characters (such as @, #, !, %, &, and so on) in the generated password.</param>
        /// <returns></returns>
        public static string GeneratePassword(int length, int numberOfNonAlphanumericCharacters)
        {
            if (length < 1 || length > 128)
            {
                throw new ArgumentException(
                    "Password length must be greater or equal to 1 and less than or equal to 128. You specified: " +
                    length, "length");
            }

            if (numberOfNonAlphanumericCharacters > length || numberOfNonAlphanumericCharacters < 0)
            {
                throw new ArgumentException(
                    "numberOfNonAlphanumericCharacters cannot be greater than length or less than 0",
                    "numberOfNonAlphanumericCharacters");
            }

            string password;
            int index;
            byte[] buf;
            char[] cBuf;
            int count;

            do
            {
                buf = new byte[length];
                cBuf = new char[length];
                count = 0;

                using (var rng = new RNGCryptoServiceProvider())
                {
                    rng.GetBytes(buf);
                }

                for (int iter = 0; iter < length; iter++)
                {
                    int i = (int) (buf[iter]%87);
                    if (i < 10)
                        cBuf[iter] = (char) ('0' + i);
                    else if (i < 36)
                        cBuf[iter] = (char) ('A' + i - 10);
                    else if (i < 62)
                        cBuf[iter] = (char) ('a' + i - 36);
                    else
                    {
                        cBuf[iter] = Punctuations[i - 62];
                        count++;
                    }
                }

                if (count < numberOfNonAlphanumericCharacters)
                {
                    int j, k;
                    var rand = new Random();
                    for (j = 0; j < numberOfNonAlphanumericCharacters - count; j++)
                    {
                        do
                        {
                            k = rand.Next(0, length);
                        } while (!Char.IsLetterOrDigit(cBuf[k]));

                        cBuf[k] = Punctuations[rand.Next(0, Punctuations.Length)];
                    }
                }

                password = new string(cBuf);
            } while (IsDangerousString(password, out index));

            return password;
        }

        internal static bool IsDangerousString(string s, out int matchIndex)
        {
            //bool inComment = false;
            matchIndex = 0;

            for (int i = 0; ;)
            {

                // Look for the start of one of our patterns 
                int n = s.IndexOfAny(StartingChars, i);

                // If not found, the string is safe
                if (n < 0) return false;

                // If it's the last char, it's safe 
                if (n == s.Length - 1) return false;

                matchIndex = n;

                switch (s[n])
                {
                    case '<':
                        // If the < is followed by a letter or '!', it's unsafe (looks like a tag or HTML comment)
                        if (IsAtoZ(s[n + 1]) || s[n + 1] == '!' || s[n + 1] == '/' || s[n + 1] == '?') return true;
                        break;
                    case '&':
                        // If the & is followed by a #, it's unsafe (e.g. &#83;) 
                        if (s[n + 1] == '#') return true;
                        break;
                }

                // Continue searching
                i = n + 1;
            }
        }

        private static bool IsAtoZ(char c)
        {
            return (c >= 'a' && c <= 'z') || (c >= 'A' && c <= 'Z');
        }
    }
}
