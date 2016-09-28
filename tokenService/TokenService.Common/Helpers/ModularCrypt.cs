using System;
using System.Diagnostics;
using System.Linq;
using System.Security.Cryptography;

namespace CareInnovations.HealthHarmony.Maestro.TokenService.Common.Helpers
{
    public static class ModularCrypt
    {
        private static readonly TimeSpan WORK = TimeSpan.FromSeconds(1);
        private const int MIN_ITERATIONS = 10000;
        private const int ROUND_ITERATIONS = 10000;

        private const int SALT_BYTES = 8; // 64-bit (OWASP)
        private const int KEY_BYTES = 20; // 160-bit (SHA1)

        private const char SEPARATOR = '$';
        private const string IDENTIFIER = "rfc2898derivebytes";

        public static bool ValidMcf(string mcf)
        {
            var valid = false;

            try
            {
                int iterations;
                byte[] salt;
                byte[] hash;

                valid = Parse(mcf, out iterations, out salt, out hash);
            }
            catch
            {
                
            }

            return valid;
        }

        private static bool Parse(string mcf, out int iterations, out byte[] salt, out byte[] hash)
        {
            if (string.IsNullOrEmpty(mcf))
            {
                throw new ArgumentNullException("mcf");
            }

            var parts = mcf.Split(new char[] { SEPARATOR }, StringSplitOptions.RemoveEmptyEntries);

            if (parts.Length != 4)
            {
                throw new FormatException("Bad format for an " + IDENTIFIER + " MCF");
            }

            if (false == parts[0].Equals(IDENTIFIER, StringComparison.InvariantCultureIgnoreCase))
            {
                throw new FormatException("Not an " + IDENTIFIER + " MCF");
            }

            try
            {
                iterations = Convert.ToInt32(parts[1]);
                salt = Convert.FromBase64String(parts[2]);
                hash = Convert.FromBase64String(parts[3]);
            }
            catch (Exception ex)
            {
                throw new FormatException("Iterations, salt or hash are invalid", ex);
            }

            return true;
        }

        /// <summary>
        /// Verify an entered password against an MCF-encoded string.
        /// </summary>
        /// <param name="password"></param>
        /// <param name="mcf"></param>
        /// <returns></returns>
        public static bool Verify(string password, string mcf)
        {
            int iterations;
            byte[] salt;
            byte[] hash;

            if (string.IsNullOrEmpty(password) || string.IsNullOrEmpty(mcf))
            {
                return false;
            }

            if (!Parse(mcf, out iterations, out salt, out hash))
            {
                return false;
            }

            var df = new Rfc2898DeriveBytes(password, salt, iterations);
            var derived = df.GetBytes(KEY_BYTES);

            // No need to compare each byte beacuse the df already slowed things down
            return hash.SequenceEqual(derived);
        }

        /// <summary>
        /// Derive a modular crypt format-encoded string from a password.
        /// </summary>
        /// <param name="password">The password.</param>
        /// <returns></returns>
        public static string Derive(string password)
        {
            var iterations = ComputeIterations(password);

            var df = new Rfc2898DeriveBytes(password, SALT_BYTES, iterations);

            return string.Concat(
                SEPARATOR,
                IDENTIFIER,
                SEPARATOR,
                df.IterationCount,
                SEPARATOR,
                Convert.ToBase64String(df.Salt),
                SEPARATOR,
                Convert.ToBase64String(df.GetBytes(KEY_BYTES))
            );
        }

        /// <summary>
        /// Extimate how many iterations can take place for a given password in the specific amount of work time.
        /// </summary>
        /// <param name="password"></param>
        /// <returns></returns>
        private static int ComputeIterations(string password)
        {
            var sw = new Stopwatch();

            var test = new Rfc2898DeriveBytes(password, SALT_BYTES, MIN_ITERATIONS);

            sw.Start();
            var bytes = test.GetBytes(KEY_BYTES);
            sw.Stop();

            var time = WORK.TotalMilliseconds / sw.ElapsedMilliseconds;

            var iterations = ((MIN_ITERATIONS * (int)time) / ROUND_ITERATIONS) * ROUND_ITERATIONS;

            if (iterations > MIN_ITERATIONS)
            {
                return iterations;
            }

            return MIN_ITERATIONS;
        }
    }
}