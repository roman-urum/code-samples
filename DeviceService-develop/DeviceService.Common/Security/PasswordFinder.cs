using Org.BouncyCastle.OpenSsl;

namespace DeviceService.Common.Security
{
    public class PasswordFinder : IPasswordFinder
    {
        public char[] GetPassword()
        {
            return "Password1,".ToCharArray();
        }
    }
}
