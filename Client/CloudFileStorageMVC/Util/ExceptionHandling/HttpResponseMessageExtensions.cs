using System.Net;
using System.Net.Http.Json;
using System.Text.Json;

namespace CloudFileStorageMVC.Util.ExceptionHandling;

public static class HttpResponseMessageExtensions
{
    public static async Task EnsureSuccessStatusCodeWithApiError(this HttpResponseMessage response)
    {
        if (!response.IsSuccessStatusCode)
        {
            var apiError = await response.Content.ReadFromJsonAsync<ApiError>()
                ?? throw new InvalidOperationException("Failed to read the API error response.");
        }
    }
}


