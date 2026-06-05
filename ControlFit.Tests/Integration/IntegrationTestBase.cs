using ControlFit.Infrastructure.Persistencia;
using Microsoft.Extensions.DependencyInjection;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

namespace ControlFit.Tests.Integration
{
    public abstract class IntegrationTestBase
    {
        protected readonly ControlFitApiFactory Factory;
        protected readonly HttpClient Client;
        protected readonly JsonSerializerOptions JsonOptions;

        protected int GimnasioId => Factory.SeedData.GimnasioId;
        protected int SuperAdminId => Factory.SeedData.SuperAdminId;
        protected int GymAdminId => Factory.SeedData.GymAdminId;
        protected string SuperAdminToken => Factory.SeedData.SuperAdminToken;
        protected string GymAdminToken => Factory.SeedData.GymAdminToken;

        protected IntegrationTestBase(ControlFitApiFactory factory)
        {
            Factory = factory;
            Client = factory.CreateClient();
            JsonOptions = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                PropertyNameCaseInsensitive = true
            };
        }

        protected AppDbContext CreateDbContext()
        {
            var scope = Factory.Services.CreateScope();
            return scope.ServiceProvider.GetRequiredService<AppDbContext>();
        }

        protected void SetAuthToken(string token)
        {
            Client.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", token);
        }

        protected void ClearAuthToken()
        {
            Client.DefaultRequestHeaders.Authorization = null;
        }

        protected async Task<T?> GetResponseData<T>(HttpResponseMessage response)
        {
            var content = await response.Content.ReadAsStringAsync();
            if (string.IsNullOrWhiteSpace(content)) return default;
            var wrapper = JsonSerializer.Deserialize<JsonElement>(content, JsonOptions);
            if (wrapper.TryGetProperty("data", out var data) && data.ValueKind == JsonValueKind.Object)
            {
                try { return JsonSerializer.Deserialize<T>(data.GetRawText(), JsonOptions); }
                catch { return default; }
            }
            if (wrapper.ValueKind == JsonValueKind.Object && !wrapper.TryGetProperty("data", out _))
            {
                try { return JsonSerializer.Deserialize<T>(content, JsonOptions); }
                catch { return default; }
            }
            return default;
        }

        protected async Task<string?> GetResponseMessage(HttpResponseMessage response)
        {
            var content = await response.Content.ReadAsStringAsync();
            if (string.IsNullOrWhiteSpace(content)) return null;
            var wrapper = JsonSerializer.Deserialize<JsonElement>(content, JsonOptions);
            if (wrapper.TryGetProperty("message", out var msg))
            {
                return msg.GetString();
            }
            if (wrapper.ValueKind == JsonValueKind.Object)
            {
                var props = new[] { "mensaje", "Message", "error", "Error" };
                foreach (var prop in props)
                {
                    if (wrapper.TryGetProperty(prop, out var val) && val.ValueKind == JsonValueKind.String)
                        return val.GetString();
                }
            }
            return null;
        }

        protected async Task<string?> GetResponseError(HttpResponseMessage response)
        {
            var content = await response.Content.ReadAsStringAsync();
            var wrapper = JsonSerializer.Deserialize<JsonElement>(content, JsonOptions);
            if (wrapper.TryGetProperty("error", out var err))
            {
                return err.GetString();
            }
            return null;
        }

        protected StringContent ToJsonContent(object obj)
        {
            var json = JsonSerializer.Serialize(obj, JsonOptions);
            return new StringContent(json, Encoding.UTF8, "application/json");
        }
    }
}
