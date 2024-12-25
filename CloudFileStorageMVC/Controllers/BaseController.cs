using CloudFileStorageMVC.Services.Token;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;
using System.Security.Claims;

namespace CloudFileStorageMVC.Controllers
{
    public class BaseController(ITokenService tokenService, IHttpClientFactory httpClientFactory) : Controller
    {
        protected readonly HttpClient httpClient = httpClientFactory.CreateClient("GetewayApiClient");
        public int GetUserId()
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            return userId;
        }

        public void GatewayClientGetToken()
        {
            var token = tokenService.GetAccessToken();
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        }

    }
}
