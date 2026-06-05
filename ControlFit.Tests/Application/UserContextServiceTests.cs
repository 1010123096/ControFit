using ControlFit.Application.Servicios;
using Microsoft.AspNetCore.Http;
using Moq;
using System.Security.Claims;

namespace ControlFit.Tests.Application
{
    public class UserContextServiceTests
    {
        [Fact]
        public void GetGimnasioId_WithClaim_ReturnsValue()
        {
            var httpContextAccessor = new Mock<IHttpContextAccessor>();
            var claims = new[] { new Claim("GimnasioId", "1") };
            var identity = new ClaimsIdentity(claims);
            var principal = new ClaimsPrincipal(identity);
            var httpContext = new DefaultHttpContext { User = principal };
            httpContextAccessor.Setup(x => x.HttpContext).Returns(httpContext);

            var service = new UserContextService(httpContextAccessor.Object);
            var result = service.GetGimnasioId();

            Assert.Equal(1, result);
        }

        [Fact]
        public void GetGimnasioId_NoClaim_ReturnsZero()
        {
            var httpContextAccessor = new Mock<IHttpContextAccessor>();
            var identity = new ClaimsIdentity();
            var principal = new ClaimsPrincipal(identity);
            var httpContext = new DefaultHttpContext { User = principal };
            httpContextAccessor.Setup(x => x.HttpContext).Returns(httpContext);

            var service = new UserContextService(httpContextAccessor.Object);
            var result = service.GetGimnasioId();

            Assert.Equal(0, result);
        }

        [Fact]
        public void GetGimnasioId_NullHttpContext_ReturnsZero()
        {
            var httpContextAccessor = new Mock<IHttpContextAccessor>();
            httpContextAccessor.Setup(x => x.HttpContext).Returns((HttpContext?)null);

            var service = new UserContextService(httpContextAccessor.Object);
            var result = service.GetGimnasioId();

            Assert.Equal(0, result);
        }

        [Fact]
        public void GetUserEmail_WithClaim_ReturnsEmail()
        {
            var httpContextAccessor = new Mock<IHttpContextAccessor>();
            var claims = new[] { new Claim("email", "test@test.com") };
            var identity = new ClaimsIdentity(claims);
            var principal = new ClaimsPrincipal(identity);
            var httpContext = new DefaultHttpContext { User = principal };
            httpContextAccessor.Setup(x => x.HttpContext).Returns(httpContext);

            var service = new UserContextService(httpContextAccessor.Object);
            var result = service.GetUserEmail();

            Assert.Equal("test@test.com", result);
        }

        [Fact]
        public void GetUserEmail_NoClaim_ReturnsEmpty()
        {
            var httpContextAccessor = new Mock<IHttpContextAccessor>();
            httpContextAccessor.Setup(x => x.HttpContext).Returns((HttpContext?)null);

            var service = new UserContextService(httpContextAccessor.Object);
            var result = service.GetUserEmail();

            Assert.Equal("", result);
        }

        [Fact]
        public void GetAdministradorId_WithClaim_ReturnsValue()
        {
            var httpContextAccessor = new Mock<IHttpContextAccessor>();
            var claims = new[] { new Claim("sub", "5") };
            var identity = new ClaimsIdentity(claims);
            var principal = new ClaimsPrincipal(identity);
            var httpContext = new DefaultHttpContext { User = principal };
            httpContextAccessor.Setup(x => x.HttpContext).Returns(httpContext);

            var service = new UserContextService(httpContextAccessor.Object);
            var result = service.GetAdministradorId();

            Assert.Equal(5, result);
        }

        [Fact]
        public void EsSuperAdmin_WhenGimnasioIdIsZero_ReturnsTrue()
        {
            var httpContextAccessor = new Mock<IHttpContextAccessor>();
            var claims = new[] { new Claim("GimnasioId", "0") };
            var identity = new ClaimsIdentity(claims);
            var principal = new ClaimsPrincipal(identity);
            var httpContext = new DefaultHttpContext { User = principal };
            httpContextAccessor.Setup(x => x.HttpContext).Returns(httpContext);

            var service = new UserContextService(httpContextAccessor.Object);
            Assert.True(service.EsSuperAdmin());
            Assert.False(service.EsAdminGimnasio());
        }

        [Fact]
        public void EsAdminGimnasio_WhenGimnasioIdIsPositive_ReturnsTrue()
        {
            var httpContextAccessor = new Mock<IHttpContextAccessor>();
            var claims = new[] { new Claim("GimnasioId", "1") };
            var identity = new ClaimsIdentity(claims);
            var principal = new ClaimsPrincipal(identity);
            var httpContext = new DefaultHttpContext { User = principal };
            httpContextAccessor.Setup(x => x.HttpContext).Returns(httpContext);

            var service = new UserContextService(httpContextAccessor.Object);
            Assert.True(service.EsAdminGimnasio());
            Assert.False(service.EsSuperAdmin());
        }
    }
}
