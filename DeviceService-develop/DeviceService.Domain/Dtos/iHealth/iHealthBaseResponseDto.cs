namespace DeviceService.Domain.Dtos.iHealth
{
    /// <summary>
    /// Contains base fields which can be retrieved in
    /// any response from iHealth API.
    /// </summary>
    public abstract class iHealthBaseResponseDto
    {
        public string Error { get; set; }

        public int? ErrorCode { get; set; }

        public string ErrorDescription { get; set; }
    }
}
