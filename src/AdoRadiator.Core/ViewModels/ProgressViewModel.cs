namespace AdoRadiator.Core.ViewModels;

public class ProgressViewModel
{
    public required string IterationName { get; set; }

    public string Header { get; set; } = null!;

    public string MessageOfTheDay { get; set; } = null!;

    public string MessageOfTheDayHeader { get; set; } = null!;

    public IterationViewModel[] Iterations { get; set; } = null!;
}
