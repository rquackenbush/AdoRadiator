using AdoRadiator.Core.Configuration;
using AdoRadiator.Core.Repositories;
using AdoRadiator.Core.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace DataLab.ProjectTools.Radiator;

public static class CronExpressions
{
    public const string MondayAndFridayAt10 = "0 10 * * 1,5";
    public const string Every15Minutes = "*/15 * * * *";
}

public class RadiatorFunction
{

    [FunctionName("RadiatorFunction")]
    public async Task TimerRun([TimerTrigger(CronExpressions.Every15Minutes)] TimerInfo myTimer, ILogger logger)
    {
        await Run(logger);
    }

#if DEBUG
    [FunctionName("DebugFunction")]
    public async Task<IActionResult> HttpRun([HttpTrigger(AuthorizationLevel.Anonymous, "get")] HttpRequest req, ILogger logger)
    {
        await Run(logger);

        return new OkResult();
    }
#endif

    private async Task Run(ILogger logger)
    {
        var configuration = RadiatorConfigurationFactory.Create();

        var adoRepository = new AdoRepository(configuration);

        var publisherService = new TeamsPublisherService(configuration);

        var radiator = new RadiatorService(configuration, publisherService, logger);

        await radiator.RadiateAsync();
    }
}
