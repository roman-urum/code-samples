using System.Collections.Generic;

namespace VitalsService.Domain.Dtos.TokenServiceDtos
{
    public class CreateUserRequest
    {
        public string Username { get; set; }

        public List<CredentialsDto> Credentials { get; set; }
    }
}
