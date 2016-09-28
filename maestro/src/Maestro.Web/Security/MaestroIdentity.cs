using System;
using System.Security.Principal;

namespace Maestro.Web.Security
{
    /// <summary>
    /// MaestroIdentity.
    /// </summary>
    /// <seealso cref="System.Security.Principal.IIdentity" />
    public class MaestroIdentity : IIdentity
    {
        private const string MaestroAuthenticationType = "MaestroUserSession";

        public string AuthenticationType
        {
            get { return MaestroAuthenticationType; }
        }

        public bool IsAuthenticated { get; private set; }

        public string Name { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="MaestroIdentity"/> class.
        /// </summary>
        public MaestroIdentity()
        {
            IsAuthenticated = false;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MaestroIdentity"/> class.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <exception cref="System.ArgumentNullException">name</exception>
        public MaestroIdentity(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                throw new ArgumentNullException("name");
            }

            Name = name;
            IsAuthenticated = true;
        }
    }
}