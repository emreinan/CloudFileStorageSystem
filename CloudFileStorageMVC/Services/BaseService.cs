using CloudFileStorageMVC.Services.Token;
using System.Net.Http.Headers;
using System.Security.Claims;

namespace CloudFileStorageMVC.Services
{
    public class BaseService(IHttpClientFactory httpClientFactory,ITokenService tokenService,IHttpContextAccessor httpContextAccessor)
    {
        protected readonly HttpClient httpClient = httpClientFactory.CreateClient("GetewayApiClient");
        public int GetUserId()
        {
            var userId = int.Parse(httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
            return userId;
        }

        public void GatewayClientGetToken()
        {
            var token = tokenService.GetAccessToken();
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        }
    }
}
