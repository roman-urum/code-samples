using System;
using System.Collections.Generic;
using System.IdentityModel.Protocols.WSTrust;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.IdentityModel.Tokens;
using SecurityAlgorithms = System.IdentityModel.Tokens.SecurityAlgorithms;
using SecurityKey = Microsoft.IdentityModel.Tokens.SecurityKey;
using SecurityTokenDescriptor = Microsoft.IdentityModel.Tokens.SecurityTokenDescriptor;
using SigningCredentials = Microsoft.IdentityModel.Tokens.SigningCredentials;

namespace SecuredWebApiClient
{
    class Program
    {
        static void Main(string[] args)
        {
            string jwt = GetJwtFromTokenIssuer();
            var client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", jwt);

            try
            {
                var result = client.GetStringAsync("http://localhost:5000/api/employees/3").Result;

                Console.WriteLine(result);

            }
            catch (AggregateException ex)
            {
                foreach (var innerException in ex.InnerExceptions)
                {
                    Console.WriteLine(innerException.Message);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            Console.ReadKey();
        }

        static string GetJwtFromTokenIssuer()
        {
            byte[] key = Convert.FromBase64String("tTW8HB0ebW1qpCmRUEOknEIxaTQ0BFCYrdjOdOl4rfM=");
            SecurityKey symetricKey = new SymmetricSecurityKey(key);
            
            var signingCredentials = new SigningCredentials(symetricKey, SecurityAlgorithms.HmacSha256Signature);
            var descriptor = new SecurityTokenDescriptor
            {                
                Issuer = "http://authzserver.demoX",
                Audience = "http://localhost:5000/api",
                Expires = DateTime.UtcNow.AddMinutes(1),
                SigningCredentials = signingCredentials,
                Subject = new ClaimsIdentity(new []
                {
                    new Claim(ClaimTypes.Name, "Johny"), 
                })
            };
            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(descriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
