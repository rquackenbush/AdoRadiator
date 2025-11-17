using System.Net.Http;
using System.Net.Http.Headers;

namespace AdoRadiator.Core.Repositories;

internal static class RepositoryCentral
{
    internal static readonly HttpClient HttpClientInstance;

    static RepositoryCentral()
    {
        HttpClientInstance = new HttpClient();

        HttpClientInstance.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
    }
}
