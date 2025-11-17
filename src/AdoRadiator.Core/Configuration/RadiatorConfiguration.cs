namespace AdoRadiator.Core.Configuration;

public class RadiatorConfiguration
{
    public required string AdoOrganization { get; init; }   

    public required string AdoProject { get; init; }

    public required string AdoTeam { get; init; }

    public required string AdoPersonalAccessToken { get; init; }

    public required string TeamsWebHookUrl { get; init; }
}
