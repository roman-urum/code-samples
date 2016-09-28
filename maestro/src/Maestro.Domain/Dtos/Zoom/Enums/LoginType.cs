namespace Maestro.Domain.Dtos.Zoom.Enums
{
    /// <summary>
    /// Type of login used in zoom.
    /// </summary>
    public enum LoginType
    {
        SNS_FACEBOOK = 0,
        SNS_GOOGLE = 1,
        SNS_API = 99,
        SNS_ZOOM = 100,
        SNS_SSO = 101
    }
}
