using AdoRadiator.Core.Configuration;
using AdoRadiator.Core.Models;
using System.Net.Http.Headers;
using System.Text.Json;

namespace AdoRadiator.Core.Repositories;

public class AdoRepository(RadiatorConfiguration configuration)
{
    private const string API_VERSION = "api-version=7.1";

    public async Task<CurrentIteration> GetCurrentIterationAsync(CancellationToken cancellationToken = default)
    {
        //Note that we specify the team during this call. 
        var url = $"https://dev.azure.com/{configuration.AdoOrganization}/{configuration.AdoProject}/{configuration.AdoTeam}/_apis/work/teamsettings/iterations?$timeframe=current&{API_VERSION}";

        var typedResponse = await GetAsync<AdoListResponse<CurrentIteration>>(url, cancellationToken);

        if (typedResponse.Value == null)
            throw new Exception($"While getting the current interation, the response from '{url}' did not contain a value.");

        return typedResponse.Value.First();
    }

    //Note that we do NOT specify the team during this call.
    public async Task<IterationNode> GetIterationNode( string[] iterationPath, int depth = 10, CancellationToken cancellationToken = default)
    {
        var joinedPath = string.Join('/', iterationPath);

        var url = $"https://dev.azure.com/{configuration.AdoOrganization}/{configuration.AdoProject}/_apis/wit/classificationnodes/Iterations/{joinedPath}?{API_VERSION}&$depth={depth}";

        return await GetAsync<IterationNode>(url, cancellationToken);
    }

    /// <summary>
    /// Performs an HTTP GET with the specified URL. Adds the PAT as a Bearer token header.
    /// </summary>
    /// <typeparam name="TResult"></typeparam>
    /// <param name="url"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    /// <exception cref="InvalidOperationException"></exception>
    private async Task<TResult> GetAsync<TResult>(string url, CancellationToken cancellationToken)
        where TResult : class
    {
        var request = new HttpRequestMessage(HttpMethod.Get, url);

        request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", configuration.AdoPersonalAccessToken);

        var response = await RepositoryCentral.HttpClientInstance.SendAsync(request, cancellationToken);

        if (!response.IsSuccessStatusCode)
            throw new Exception($"Request failed: {response.StatusCode} {response.ReasonPhrase}");

        var responseText = await response.Content.ReadAsStringAsync();

        var serializerOptions = new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        };

        return JsonSerializer.Deserialize<TResult>(responseText, serializerOptions) ?? throw new InvalidOperationException("Result was null.");
    }
}
