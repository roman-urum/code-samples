using System;
using System.Security.Cryptography.X509Certificates;
using System.Threading;

namespace HealthLibrary.Web.Api.Security
{
    /// <summary>
    /// CertificateStorage.
    /// </summary>
    public static class CertificateStorage
    {
        private const string CertificatePath = @"{0}\App_Data\server.cer";
        private static readonly object synclock = new object();

        public static readonly X509Certificate ServerCertificate;

        /// <summary>
        /// Initializes the <see cref="CertificateStorage"/> class.
        /// </summary>
        static CertificateStorage()
        {
            if (ServerCertificate == null)
            {
                lock (synclock)
                {
                    if (ServerCertificate == null)
                    {
                        var certificate = ReadServerCertificate();

                        Thread.MemoryBarrier();

                        ServerCertificate = certificate;
                    }
                }
            }
        }

        /// <summary>
        /// Reads server sertificate from file.
        /// </summary>
        /// <returns></returns>
        private static X509Certificate ReadServerCertificate()
        {
            var certPath = string.Format(CertificatePath, AppDomain.CurrentDomain.BaseDirectory);

            return X509Certificate2.CreateFromCertFile(certPath);
        }
    }
}