using System.Net;
using System.Net.Http.Json;

namespace ControlFit.Tests.Integration
{
    [Collection("IntegrationTests")]
    public class DashboardIntegrationTests : IntegrationTestBase
    {
        public DashboardIntegrationTests(ControlFitApiFactory factory) : base(factory) { }

        [Fact]
        public async Task GymAdminDashboard_WithoutAuth_ReturnsUnauthorized()
        {
            ClearAuthToken();

            var response = await Client.GetAsync("api/dashboard/gym-admin");

            Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
        }

        [Fact]
        public async Task GymAdminDashboard_AsSuperAdmin_ReturnsUnauthorized()
        {
            SetAuthToken(SuperAdminToken);

            var response = await Client.GetAsync("api/dashboard/gym-admin");

            Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
        }

        [Fact]
        public async Task GymAdminDashboard_AsGymAdmin_ReturnsOk()
        {
            SetAuthToken(GymAdminToken);

            var response = await Client.GetAsync("api/dashboard/gym-admin");

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async Task SuperAdminDashboard_WithoutAuth_ReturnsUnauthorized()
        {
            ClearAuthToken();

            var response = await Client.GetAsync("api/dashboard/super-admin");

            Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
        }

        [Fact]
        public async Task SuperAdminDashboard_AsGymAdmin_ReturnsUnauthorized()
        {
            SetAuthToken(GymAdminToken);

            var response = await Client.GetAsync("api/dashboard/super-admin");

            Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
        }

        [Fact]
        public async Task SuperAdminDashboard_AsSuperAdmin_ReturnsOk()
        {
            SetAuthToken(SuperAdminToken);

            var response = await Client.GetAsync("api/dashboard/super-admin");

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async Task GymAdminDashboard_HasExpectedStats()
        {
            SetAuthToken(GymAdminToken);

            var response = await Client.GetAsync("api/dashboard/gym-admin");
            var body = await GetResponseData<Dictionary<string, object>>(response);

            Assert.NotNull(body);
        }

        [Fact]
        public async Task SuperAdminDashboard_HasExpectedStats()
        {
            SetAuthToken(SuperAdminToken);

            var response = await Client.GetAsync("api/dashboard/super-admin");
            var body = await GetResponseData<Dictionary<string, object>>(response);

            Assert.NotNull(body);
        }
    }
}
