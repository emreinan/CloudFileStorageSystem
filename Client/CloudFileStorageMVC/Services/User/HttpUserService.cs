using CloudFileStorageMVC.Dtos.User;
using CloudFileStorageMVC.Util.ExceptionHandling;
using System.Net.Http;

namespace CloudFileStorageMVC.Services.User
{
    public class HttpUserService(IHttpClientFactory httpClientFactory) : IUserService
    {
        private readonly HttpClient _httpClient = httpClientFactory.CreateClient("GetewayApiClient");

        public async Task<UserDto> GetUserById(int id)
        {
            var response = await _httpClient.GetAsync($"/api/User/{id}");
            await response.EnsureSuccessStatusCodeWithApiError();
            return await response.Content.ReadFromJsonAsync<UserDto>();
        }

        public async Task<List<UserDto>> GetUsersAsync()
        {
            var response = await _httpClient.GetAsync("/api/User");
            await response.EnsureSuccessStatusCodeWithApiError();
            return await response.Content.ReadFromJsonAsync<List<UserDto>>();   
        }
    }
}
