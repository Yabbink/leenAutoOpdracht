using GraafschapCollege.Shared.Options;
using GraafschapCollege.Shared.Responses;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace GraafschapCollege.Shared.Clients
{
    public class UserHttpClient
    {
        private readonly HttpClient client;

        public UserHttpClient(IHttpClientFactory httpClientFactory, IOptions<ApiOptions> options)
        {
            client = httpClientFactory.CreateClient(nameof(UserHttpClient));
            client.BaseAddress = new Uri($"{options.Value.BaseUrl}/users");
        }

        public async Task<UserResponse?> GetUserAsync(int id)
        {
            var response = await client.GetAsync(id.ToString());

            if (!response.IsSuccessStatusCode)
            {
                return null;
            }

            var content = await response.Content.ReadAsStringAsync();
            var user = JsonSerializer.Deserialize<UserResponse>(content, JsonOptions.SerializerOptions);

            return user;
        }

        public async Task<IEnumerable<UserResponse>> GetUsersAsync()
        {
            var response = await client.GetAsync(string.Empty);

            if (!response.IsSuccessStatusCode)
            {
                return [];
            }

            var content = await response.Content.ReadAsStringAsync();
            var users = JsonSerializer.Deserialize<IEnumerable<UserResponse>>(content, JsonOptions.SerializerOptions);

            if (users is null)
            {
                return [];
            }

            return users;
        }
    }
}
