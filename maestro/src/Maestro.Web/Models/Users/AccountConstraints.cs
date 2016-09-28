namespace Maestro.Web.Models.Users
{
    /// <summary>
    /// Container for constant values of common users' fields.
    /// </summary>
    public static class AccountConstraints
    {
        public const int PasswordMinLength = 8;

        public const string PasswordComplexityPattern =
            "^(?=.*[a-z])(?=.*[A-Z])(?=.*[-+_!@#$%^&*.,?`()={};:,?<>~'\"|/[\\]\\\\ ]).+$";
    }
}