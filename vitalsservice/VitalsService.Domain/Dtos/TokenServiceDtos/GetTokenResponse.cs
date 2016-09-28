﻿namespace VitalsService.Domain.Dtos.TokenServiceDtos
{
    public class GetTokenResponse
    {
        /// <summary>
        /// User token
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// Authentication result
        /// </summary>
        public bool Success { get; set; }

        public int RemainingSeconds { get; set; }
    }
}
