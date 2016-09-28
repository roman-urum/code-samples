using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading;
using Org.BouncyCastle.Asn1;
using Org.BouncyCastle.Asn1.Pkcs;
using Org.BouncyCastle.Asn1.X509;
using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Math;
using Org.BouncyCastle.OpenSsl;
using Org.BouncyCastle.Pkcs;
using Org.BouncyCastle.Security;
using Org.BouncyCastle.X509;
using X509Certificate = System.Security.Cryptography.X509Certificates.X509Certificate;

namespace DeviceService.Common.Security
{
    public class SertificateGenerator
    {
        private const string CertificatePath = @"{0}\App_Data\server.cer";
        private static readonly object synclock = new object();

        public static readonly X509Certificate ServerCertificate;

        static SertificateGenerator()
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
        /// Generates client certificate by certificate signing request.
        /// </summary>
        /// <param name="csrBytes">CSR as bytes array</param>
        /// <param name="commonName">Common name of certificate</param>
        /// <exception cref="InvalidCastException">Invalid format of CSR</exception>
        /// <returns></returns>
        public static X509Certificate2 SignRequest(byte[] csrBytes, string commonName)
        {
            if (string.IsNullOrEmpty(commonName))
            {
                throw new ArgumentNullException("commonName");
            }

            var certificationRequest = new Pkcs10CertificationRequest(csrBytes);
            CertificationRequestInfo csrInfo = certificationRequest.GetCertificationRequestInfo();
            SubjectPublicKeyInfo pki = csrInfo.SubjectPublicKeyInfo;

            AsymmetricKeyParameter publicKey = PublicKeyFactory.CreateKey(pki);

            // Version1 (No Extensions) Certificate
            DateTime startDate = DateTime.UtcNow;
            DateTime expiryDate = startDate.AddYears(100);
            var serialNumber = new BigInteger(32, new Random());

            var certGen = new X509V1CertificateGenerator();
            var x509ServerCertificate = ServerCertificate;
            var caCert = DotNetUtilities.FromX509Certificate(x509ServerCertificate);

            certGen.SetSerialNumber(serialNumber);
            certGen.SetIssuerDN(caCert.SubjectDN);
            certGen.SetNotBefore(startDate);
            certGen.SetNotAfter(expiryDate);
            certGen.SetSubjectDN(CreateSubject(commonName));
            certGen.SetSignatureAlgorithm("SHA256withRSA");
            certGen.SetPublicKey(publicKey);

            var keyPath = string.Format(@"{0}\App_Data\server.key", AppDomain.CurrentDomain.BaseDirectory);

            AsymmetricCipherKeyPair keyPair;
            using (var reader = File.OpenText(keyPath))
            {
                keyPair = (AsymmetricCipherKeyPair)new PemReader(reader, new PasswordFinder()).ReadObject();
            }

            Org.BouncyCastle.X509.X509Certificate cert = certGen.Generate(keyPair.Private);

            return new X509Certificate2(DotNetUtilities.ToX509Certificate(cert));
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

        private static X509Name CreateSubject(string commonName)
        {
            var attributes = new Dictionary<DerObjectIdentifier, string>
            {
                { X509Name.CN, commonName}
            };

            return new X509Name(attributes.Keys.ToList(), attributes);
        }
    }
}
