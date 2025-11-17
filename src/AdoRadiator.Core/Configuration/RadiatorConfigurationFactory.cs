namespace AdoRadiator.Core.Configuration;

public static class RadiatorConfigurationFactory
{
    public static RadiatorConfiguration Create()
    {
        return new RadiatorConfiguration
        {
            AdoOrganization = GetSetting("ADO_ORGANIZATION"),
            AdoProject = GetSetting("ADO_PROJECT"),
            AdoTeam = GetSetting("ADO_TEAM"),
            AdoPersonalAccessToken = GetSetting("ADO_PERSONAL_ACCESS_TOKEN"),
            TeamsWebHookUrl = GetSetting("TEAMS_WEBHOOK_URL")
        };
    }

    private static string GetSetting(string variable)
    {
        var value = Environment.GetEnvironmentVariable(variable);

        if (string.IsNullOrEmpty(value))
            throw new Exception($"The environment variable '{variable}' was not set.");

        return value;
    }
}
