using Microsoft.AspNetCore.Http;
using Shared.RequestFeatures;
using System.Text.Json;

namespace MovieApp.Presentation.Extensions
{
    public static class ResponseExtension
    {
        public static void AddPaginationHeader(this HttpResponse response, MetaData metaData)
        {
            response.Headers.Add("X-Pagination", JsonSerializer.Serialize(metaData));
        }
    }
}
