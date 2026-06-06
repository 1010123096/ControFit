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
            Assert.Equal("Admin Gimnasio", claims["role"]);
            Assert.Equal("5", claims["GimnasioId"]);
            Assert.Equal("ControlFit", jwt.Issuer);
            Assert.Contains("ControlFitApp", jwt.Audiences);
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

        [Fact]
        public void GenerarToken_WithNombreGimnasio_AddsClaim()
        {
            var service = CreateTokenService();
            var token = service.GenerarToken(1, "admin@test.com", 5, "FitZone");

            var jwt = new JwtSecurityToken(token);
            var claims = jwt.Claims.ToDictionary(c => c.Type, c => c.Value);

            Assert.Equal("FitZone", claims["nombreGimnasio"]);
        }

        [Fact]
        public void GenerarToken_NullNombreGimnasio_DoesNotAddClaim()
        {
            var service = CreateTokenService();
            var token = service.GenerarToken(1, "admin@test.com", 5, null);

            var jwt = new JwtSecurityToken(token);

            Assert.DoesNotContain(jwt.Claims, c => c.Type == "nombreGimnasio");
        }

        [Fact]
        public void GenerarToken_EmptyNombreGimnasio_DoesNotAddClaim()
        {
            var service = CreateTokenService();
            var token = service.GenerarToken(1, "admin@test.com", 5, "");

            var jwt = new JwtSecurityToken(token);

            Assert.DoesNotContain(jwt.Claims, c => c.Type == "nombreGimnasio");
        }

        [Fact]
        public void GenerarToken_NullGimnasioId_SetsSuperAdminRole()
        {
            var service = CreateTokenService();
            var token = service.GenerarToken(1, "super@test.com");

            var jwt = new JwtSecurityToken(token);
            var claims = jwt.Claims.ToDictionary(c => c.Type, c => c.Value);

            Assert.Equal("Super Admin", claims["role"]);
        }

        [Fact]
        public void GenerarToken_WithGimnasioId_SetsAdminGimnasioRole()
        {
            var service = CreateTokenService();
            var token = service.GenerarToken(1, "admin@test.com", 3);

            var jwt = new JwtSecurityToken(token);
            var claims = jwt.Claims.ToDictionary(c => c.Type, c => c.Value);

            Assert.Equal("Admin Gimnasio", claims["role"]);
        }

        [Fact]
        public void GenerarToken_IssuerAndAudience_AreSet()
        {
            var service = CreateTokenService();
            var token = service.GenerarToken(1, "test@test.com");

            var jwt = new JwtSecurityToken(token);

            Assert.Equal("ControlFit", jwt.Issuer);
            Assert.Contains("ControlFitApp", jwt.Audiences);
        }

        [Fact]
        public void GenerarToken_SubClaim_MatchesAdministradorId()
        {
            var service = CreateTokenService();
            var token = service.GenerarToken(42, "test@test.com");

            var jwt = new JwtSecurityToken(token);
            var sub = jwt.Claims.FirstOrDefault(c => c.Type == JwtRegisteredClaimNames.Sub);

            Assert.NotNull(sub);
            Assert.Equal("42", sub.Value);
        }

        [Fact]
        public void GenerarToken_EmailClaim_MatchesCorreo()
        {
            var service = CreateTokenService();
            var token = service.GenerarToken(1, "custom@email.com");

            var jwt = new JwtSecurityToken(token);
            var email = jwt.Claims.FirstOrDefault(c => c.Type == JwtRegisteredClaimNames.Email);

            Assert.NotNull(email);
            Assert.Equal("custom@email.com", email.Value);
        }
    }
}
