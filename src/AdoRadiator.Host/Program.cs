using AdoRadiator.Core.Configuration;
using AdoRadiator.Core.Repositories;
using AdoRadiator.Core.Services;
using Microsoft.Extensions.Logging;

var loggerFactory = new LoggerFactory();

loggerFactory.AddConsole();

var logger = loggerFactory.CreateLogger("ConsoleHost");

var configuration = RadiatorConfigurationFactory.Create();

var publisherService = new TeamsPublisherService(configuration);

var radiatorService = new RadiatorService(configuration, publisherService, logger);

await radiatorService.RadiateAsync();
