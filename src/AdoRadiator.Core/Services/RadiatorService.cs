using AdoRadiator.Core.Configuration;
using AdoRadiator.Core.Factories;
using AdoRadiator.Core.Helpers;
using AdoRadiator.Core.Models;
using AdoRadiator.Core.Repositories;
using AdoRadiator.Core.ViewModels;
using FabQuack.TreeLib;
using Microsoft.Extensions.Logging;

namespace AdoRadiator.Core.Services;

public class RadiatorService(RadiatorConfiguration configuration, IPublisherService publisherService, ILogger logger)
{
    public async Task RadiateAsync()
    {
        var adoRepository = new AdoRepository(configuration);
        var messageRenderingService = new MessageRenderingService();

        try
        {
            var currentIteration = await adoRepository.GetCurrentIterationAsync();

            if (currentIteration.Attributes == null)
                throw new Exception("Attributes was not when getting the current iteration.");

            logger.LogInformation("Iteration {Name}:", currentIteration.Name);
            logger.LogInformation("  Path: {Path}", currentIteration.Path);
            logger.LogInformation("  {StartDate} to {FinishDate}", currentIteration.Attributes.StartDate, currentIteration.Attributes.FinishDate);
            logger.LogInformation("  Timeframe: {TimeFrame}", currentIteration.Attributes.TimeFrame);


            var sprintDays = SprintDayFactory.GetSprintDays(currentIteration.Attributes.StartDate, currentIteration.Attributes.FinishDate)
                .ToArray();

            var sprintDay = sprintDays.FirstOrDefault(sd => sd.Date == DateTime.Now.Date);

            var messageViewModel = new ProgressViewModel
            {
                IterationName = currentIteration.Name
            };


            if (sprintDay == null)
            {
                messageViewModel.Header = "Why did you run this on a weekend? You're weird.";
            }
            else
            {
                if (sprintDay.Number == 1)
                {
                    messageViewModel.Header = "Today is the first day of the sprint!!!";
                    messageViewModel.MessageOfTheDayHeader = "State sanctioned motivational message:";
                    messageViewModel.MessageOfTheDay = MessageHelper.GetRandomMessage(MessageFilePaths.FirstDay);
                }
                else if (sprintDay.Number == sprintDays.Length)
                {
                    messageViewModel.Header = "Today is the last day of the sprint!!! Wrap it up folks!";
                    messageViewModel.MessageOfTheDayHeader = "State sanctioned motivational message:";
                    messageViewModel.MessageOfTheDay = MessageHelper.GetRandomMessage(MessageFilePaths.LastDay);
                }
                else if (sprintDay.Number == sprintDays.Length / 2)
                {
                    messageViewModel.Header = "We're half way through the sprint people!!!! This is not a drill!!";
                    messageViewModel.MessageOfTheDayHeader = "State sanctioned motivational message:";
                    messageViewModel.MessageOfTheDay = MessageHelper.GetRandomMessage(MessageFilePaths.MidPoint);
                }
                else
                {
                    messageViewModel.Header = $"Today is day {sprintDay.Number} of {sprintDays.Length}";
                    messageViewModel.MessageOfTheDayHeader = "Approved trivia for your amusement:";
                    messageViewModel.MessageOfTheDay = MessageHelper.GetRandomMessage(MessageFilePaths.Trivia);
                }
            }

            var rootIterationPath = PathHelper.Split(currentIteration.Path)
                .Skip(1)
                .Take(1)
                .ToArray();

            var rootIteration = await adoRepository.GetIterationNode(rootIterationPath);

            var rootIterations = new Dictionary<Guid, IterationNode>
        {
            { rootIteration.Identifier, rootIteration }
        };

            var iterationTree = rootIterations.ToImmutableKeyedTree(kvp => GetChildren(kvp));

            if (iterationTree.TryGetNode(currentIteration.Id, out var currentNode))
            {
                var lineage = currentNode.GetParentValues(true)
                    .Reverse()
                    .ToArray();

                messageViewModel.Iterations = lineage.Select(x =>
                        new IterationViewModel
                        {
                            Name = x.Name,
                            StartDate = x.Attributes.StartDate.ToString("d"),
                            FinishDate = x.Attributes.FinishDate.ToString("d"),
                            Progress = Math.Round(ProgressHelper.GetProgress(x.Attributes.StartDate, x.Attributes.FinishDate)),
                            Color = ProgressHelper.GetColorFromProgress(ProgressHelper.GetProgress(x.Attributes.StartDate, x.Attributes.FinishDate))
                        }).ToArray();


                var teamsMessageBody = messageRenderingService.RenderMessage(messageViewModel);

                await publisherService.PublishAsync(teamsMessageBody);
            }
            else
            {
                throw new Exception("Unable to find the current sprint in the tree.");
            }
        }
        catch(Exception ex)
        {
            var message = messageRenderingService.RenderException(ex);

            await publisherService.PublishAsync(message);
        }
    }

    private static IEnumerable<KeyValuePair<Guid, IterationNode>> GetChildren(KeyValuePair<Guid, IterationNode> parent)
    {
        if (parent.Value.Children != null)
        {
            foreach (var child in parent.Value.Children)
            {
                yield return new KeyValuePair<Guid, IterationNode>(child.Identifier, child);    
            }
        }
    }
}
