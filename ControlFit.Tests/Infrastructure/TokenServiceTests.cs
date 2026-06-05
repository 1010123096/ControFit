using ControlFit.Infrastructure.Repository;
using Microsoft.Extensions.Configuration;
using System.IdentityModel.Tokens.Jwt;

namespace ControlFit.Tests.Infrastructure
{
    public class TokenServiceTests
    {
        private static TokenService CreateTokenService(string key = "MySuperSecretKeyForJwtTokenGeneration2026!")
        {
            var config = new ConfigurationBuilder()
                .AddInMemoryCollection(new Dictionary<string, string?>
                {
                    ["Jwt:Key"] = key,
                    ["Jwt:Issuer"] = "ControlFit",
                    ["Jwt:Audience"] = "ControlFitApp"
                })
                .Build();
            return new TokenService(config);
        }

        [Fact]
        public void GenerarToken_WithGimnasioId_ReturnsValidToken()
        {
            var service = CreateTokenService();
            var token = service.GenerarToken(1, "admin@test.com", 5);

            var jwt = new JwtSecurityToken(token);

            var claims = jwt.Claims.ToDictionary(c => c.Type, c => c.Value);
            Assert.Equal("admin@test.com", claims["email"]);
            Assert.Equal("1", claims["sub"]);
            Assert.Equal("Administrador", claims["http://schemas.microsoft.com/ws/2008/06/identity/claims/role"]);
            Assert.Equal("5", claims["GimnasioId"]);
            Assert.Equal("ControlFit", jwt.Issuer);
        }

        [Fact]
        public void GenerarToken_NullGimnasioId_ReturnsZeroClaim()
        {
            var service = CreateTokenService();
            var token = service.GenerarToken(2, "super@test.com", null);

            var jwt = new JwtSecurityToken(token);

            var claims = jwt.Claims.ToDictionary(c => c.Type, c => c.Value);
            Assert.Equal("0", claims["GimnasioId"]);
        }

        [Fact]
        public void GenerarToken_ExpiresInSixHours()
        {
            var service = CreateTokenService();
            var before = DateTime.UtcNow;
            var token = service.GenerarToken(1, "test@test.com");
            var after = DateTime.UtcNow;

            var jwt = new JwtSecurityToken(token);

            Assert.True(jwt.ValidFrom <= before.AddMinutes(1));
            Assert.True(jwt.ValidTo >= after.AddHours(6).AddMinutes(-1));
            Assert.True(jwt.ValidTo <= after.AddHours(6).AddMinutes(1));
        }
    }
}
