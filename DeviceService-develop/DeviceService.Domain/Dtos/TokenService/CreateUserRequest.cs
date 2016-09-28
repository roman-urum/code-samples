using System.Collections.Generic;

namespace DeviceService.Domain.Dtos.TokenService
{
    public class CreateUserRequest
    {
        public string Username { get; set; }

        public List<CredentialsDto> Credentials { get; set; }
    }
}
