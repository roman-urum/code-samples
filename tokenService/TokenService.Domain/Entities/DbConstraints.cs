namespace CareInnovations.HealthHarmony.Maestro.TokenService.Domain.Entities
{
    public static class DbConstraints
    {
        public static class MaxLength
        {
            public const int CredentialValue = 256;
            public const int GroupName = 100;
            public const int GroupDescription = 1000;
            public const int ServiceName = 50;
            public const int PolicyName = 100;
            public const int ControllerName = 50;
            public const int Username = 256;
            public const int PrincipalDescription = 1000;
            public const int FirstName = 50;
            public const int LastName = 50;
        }
    }
}
