using AdoRadiator.Core.Configuration;

namespace AdoRadiator.Core.Repositories;

public class TeamsPublisherService(RadiatorConfiguration configuration) : IPublisherService
{
    public async Task PublishAsync(string body)
    {
        var request = new HttpRequestMessage(HttpMethod.Post, configuration.TeamsWebHookUrl);

        request.Content = new StringContent(body);

        var response = await RepositoryCentral.HttpClientInstance.SendAsync(request);

        if (!response.IsSuccessStatusCode)
            throw new Exception($"Failed to post teams message {response.StatusCode} {response.ReasonPhrase}");
    }
}
