namespace AdoRadiator.Core.ViewModels;

public class IterationViewModel
{
    /// <summary>
    /// The name of the iteration.
    /// </summary>
    public required string Name { get; set; }

    /// <summary>
    /// Text formatted start date.
    /// </summary>
    public required string StartDate { get; set; }

    /// <summary>
    /// Test formmatted finish date.
    /// </summary>
    public required string FinishDate { get; set; }

    /// <summary>
    /// A number from 0 to 100.
    /// </summary>
    public required double Progress { get; set; }

    /// <summary>
    /// The color of the ieration (applied to the progress bar). Valid values are "Accent", "Good", "Warning", "Attention" /// </summary>
    public required string Color { get; set; }
}
