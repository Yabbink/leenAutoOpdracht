using GraafschapCollege.Shared.Options;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using static GraafschapCollege.Shared.Responses.ReservationResponse;

namespace GraafschapCollege.Shared.Clients
{
    public class VehicleHttpClient
    {
        private readonly HttpClient client;

        public VehicleHttpClient(IHttpClientFactory httpClientFactory, IOptions<ApiOptions> options)
        {
            client = httpClientFactory.CreateClient(nameof(VehicleHttpClient));
            client.BaseAddress = new Uri($"{options.Value.BaseUrl}/vehicles");
        }

        public async Task<IEnumerable<VehicleResponse>> GetVehiclesAsync()
        {
            var response = await client.GetAsync(string.Empty);

            if (!response.IsSuccessStatusCode)
            {
                return [];
            }

            var content = await response.Content.ReadAsStringAsync();
            var vehicles = JsonSerializer.Deserialize<IEnumerable<VehicleResponse>>(content, JsonOptions.SerializerOptions);

            if (vehicles is null)
            {
                return [];
            }

            return vehicles;
        }
    }
}
