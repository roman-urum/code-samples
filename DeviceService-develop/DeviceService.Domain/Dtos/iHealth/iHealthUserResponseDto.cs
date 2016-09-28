using Newtonsoft.Json;

namespace DeviceService.Domain.Dtos.iHealth
{
    /// <summary>
    /// Model for data in response with iHealth user details.
    /// </summary>
    public class iHealthUserResponseDto : iHealthBaseResponseDto
    {
        public string APIName { get; set; }

        public string AccessToken { get; set; }

        public int Expires { get; set; }

        public string RefreshToken { get; set; }

        public int RefreshTokenExpires { get; set; }

        public string UserOpenID { get; set; }

        [JsonProperty("client_para")]
        public string СlientPara { get; set; }
    }
}
